using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public static class AsyncUtil
{
    /// <summary>
    /// 非同期で画像を読み込む
    /// </summary>
    /// <param name="path">読み込みたい画像のPath</param>
    /// <returns>SpriteData</returns>
    public static async Task<Texture2D> LoadAsTextureAsync(string path)
    {
        byte[] result;
        Texture2D texture = new Texture2D(1, 1);
        try{
            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                result = new byte[fs.Length];
                await fs.ReadAsync(result, 0, (int)fs.Length);
            }
            texture.LoadImage(result);
        }
        catch(FileNotFoundException)
        {
            Debug.Log("Fileが見つかりませんでした．\n" + Path.GetFileNameWithoutExtension(path));
        }
        catch(Exception)
        {
            Debug.Log("Textureのロードができませんでした．\n" + Path.GetFileNameWithoutExtension(path));
        }

        return texture;
    }


    public static async Task<Sprite> LoadAsSpriteAsync(string path)
    {
        Texture2D       texture = await LoadAsTextureAsync(path);

        Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        Debug.Log("Path Name: " + Path.GetFileName(path));
        // sprite.name = Path.GetFileName(path).Replace(Path.GetExtension(path), string.Empty);

        return sprite;
    }
}
