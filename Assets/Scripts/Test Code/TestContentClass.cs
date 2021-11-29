using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// テスト用のクラス
/// </summary>
public class TestContent
{
    public string thumbnailName;
    public int number;
    public TestContent(int num)
    {
        this.thumbnailName = "icon" + (num % 30).ToString("000") + ".png";
        this.number = num;
    }
}

public static class TestContentDatabase
{
    public static List<TestContent> contentList = new List<TestContent>();

    public static void SetupDatabase(int contentNum)
    {
        // テスト用コンテンツの初期化
        contentList = new List<TestContent>();
        for(int i = 0; i < contentNum; i++)
        {
            TestContent content = new TestContent(i);
            contentList.Add(content);
        }
    }
}