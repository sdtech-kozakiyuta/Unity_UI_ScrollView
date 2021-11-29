# Unity で使える Scroll View
## 概要
![sample movie](https://user-images.githubusercontent.com/37802825/105457444-322ea900-5cca-11eb-8026-93143c568db7.gif)

添付動画の様なスクロールビューを構築するためのプレハブ等を提供．  
数個のGameObjectを使いまわして膨大な量のコンテンツをリストで表示できるアルゴリズムを活用し，メモリの圧迫を抑える実装をしている．
＜～さん＞のスクリプトを参考にした．  
このアルゴリズムを活用して，有限でも無限でも対応できるリストを作成することができる．  
本リポジトリでは，フィルタなどでリスト内コンテンツが動的に変わる場合に対応できるよう改良したスクリプトを提供している．（動画参照）  
このスクリプトの一部は"Mazda Drive Viewer"で利用されている．（リリース後URL載せます．）

## サンプルシーンの説明
- SampleScene_1
    - 有限リストのサンプルを表示するシーン．
    - ヒエラルキー上の"SceneController"より表示させるコンテンツの総数を指定でき，この指定された数字の分だけ，コンテンツのインスタンスができる．
- SampleScene_2
    - 無限リストのサンプルを表示するシーン．
    - 今回の場合は例としてカレンダーを表示．

## スクリプトの説明

### ざっくりクラス図
![Class diagram](https://github.com/sdtech-kozakiyuta/Unity_UI_ScrollView/blob/main/Reference/class_diagram.png)

本スクリプトはMVCモデルを意識して実装している．"ViewController"がMVCにおける"Controller"の役割をになっており，外部のデータベースから表示させたいコンテンツデータをとってきて，MVCの"View"に該当する"InfiniteScroll", "LimitedScrollController", "ScrollViewItem"を介して表示を切り替える．

- InfiniteScroll
    - ScrollViewで表示するGameObjectを管理するクラス．
- LimitedScrollController
    - ScrollViewの表示を操作したい場合，ここで提供されるメソッドを通じてScrollViewの表示を変える．
- ScrollViewItem
    - ScrollView上で展開される要素．このクラスを通じて，コンテンツの中身を更新するタイミングを取得
- ViewController
    - MVCでいうControllerの部分．表示したい情報を管理し，それをもとにScrollViewの表示を切り替える．LimitedScrollControllerを通じてViewを更新．

### ざっくりシーケンス図
#### 起動時
![Sequence diagram](https://github.com/sdtech-kozakiyuta/Unity_UI_ScrollView/blob/main/Reference/class_diagram.png)

#### フィルタでリストのコンテンツを操作した時
![Sequence diagram](https://github.com/sdtech-kozakiyuta/Unity_UI_ScrollView/blob/main/Reference/Sequence_diagram_OnFilter.png)
