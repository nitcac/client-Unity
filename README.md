# client-Unity
サイバーエージェント「平成最後のハッカソン」にて開発したプロダクトのクライアントサイドのリポジトリです．

# バージョン
Unity 2018.3.2f1

# エディタインストール
上記のUnityエディタの他，AR用開発環境「Vuforia」，「Android Build」サポートをインストーラーから追加でインストールする．
Vuforiaについて：https://developer.vuforia.com/

# ディレクトリ構成
http://www.project-unknown.jp/entry/2017/06/04/044524 から参考
## {$GameName} 
各ミニゲームの名前のディレクトリを一つ作って，開発するものをこの中に全部突っ込む．「Scripts」「Scene」「Prefabs」「Animations」等．

### AR_Gengo
ARを使った元号発表リズムゲーム．Vuforia及びカメラ内蔵（or Webカメラ）のPCが必要です．
Androidにビルドした場合外側のカメラが起動します．インカメにしたかったけどVuforiaのドキュメントからうまく書けなかった．

### Menu
最初のタイトル画面．各ミニゲームに遷移する．
タイトルのテキストが動くのはアニメーション機能で動かしてる．

### Quiz
クイズゲーム．現状問題をRigidbodyに重力をつけて落下させており，デバイス事に落下スピードが異なる不具合あり．

### TowerBattle
平成タワーバトル．ランキング機能が未実装（サーバー側にPostとGetは実装しているっぽいがUnity側でJsonのテキストを受け取れない不具合があり）

## Common
ゲーム全体で共通で使える素材，スクリプトを入れる予定．UIとかそうだよね

# アセットについて
このプロジェクトはUnityアセットストアなどの素材は使っていません．
外部から取ったフリー素材（リポジトリ内にあり）のみです
