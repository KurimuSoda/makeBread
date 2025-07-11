# Maybe Bread 制作記録(2024/07/29 ~ )
<article>
  <p>制作の記録を付けようと思います。最終的には操作方法などもまとめるつもりです。</p>
</article>
</br>

<h2>2025/3/15 (2025/5/14記載) 　現時点での操作方法と接続確認方法</h2>
<article>
  <h3>操作方法(デバイス)</h3>
  操作説明は縦振りで消えると思います
  
  - 縦振り
  - RFIDユニット(白いやつ)にNFCタグをタッチ <-- 素材登録シーンのみに限定中...のはず
  - M5ボタン(一番大きいボタン) : 選択した素材を決定(キーボードのIと同じ)
  
  <br>
  <h3>操作方法(キーボード)</h3>
  操作説明は、 Space or Enter(Return) で消えるはずです
  
  - N : 次のシーンへ遷移(全シーン共通)
  - 素材選択シーン
    * I : 素材の決定(画面左に追加される)
    * 数字キー 0 ~ 9 + α : 素材の選択(ポップアップが出る)
    * Enter(Return) : 選択を終わる(次のシーンへ遷移する)
  - 火加減調節シーン
    * S : デバイスの縦振り１回分、火が大きくなる<br>
  
  
    <br><br>

  <h3>接続確認方法(Unity)</h3>
  　unityエディタでゲームを再生する前に、M5CPlus2の電源を入れておく必要があります。<br>
  　unityエディタでゲーム再生後、"Resource busy"のようなログが出なければデバイスとゲームが接続されています。<br>
  　unityエディタでゲーム停止後は、M5CPlus2の電源を切るまで再生停止できないので都度M5CPlu2の電源を切ってください。<br>
</article><br><br>



<h2>2024/08/04 (2024/08/05記載) イベント展示</h2>
<artile>

  - 大学のイベントに展示させてもらいました
  - 初期化が紐づいていなかったので新たに初期化のスクリプトを改造中
  <p>小学生を中心に42回分遊んでもらいました。たまにリザルトが表示されないバグが発生しました。NFCのタグが1つ紛失しました。アイテムとして登録しているタグだったので、別のタグに変更する必要があります。</p>
</artile>

<h2>2024/08/03記載</h2>
<article>

  - BGMがスクリプトの構成を変えた影響で初期化されなくなっていたので初期化するようにしました。
  <p>修正できていないバグが潜んでいそうで怖いです。ビルドしてゲームを動かすと動きが重く?なってしまうのでテクノフェアでもUnityの画面で出そうと思います。</p>
</article></br></br>

<h2>2024/08/02記載</h2>
<article>

  - オーブンの画面の配置を変えました
    * 画面内にパンの様子が見れるワイプを設置して炎を調節することをメインにする配置にしました。
  - オーブンの画面内にワイプ画面を新設しました
  - オーブン内のパンにちょうど良い火加減の時だけエフェクトが表示されるようになりました
  - 焼き加減の時間によってステータスがGameMGに記録されるようになりました
      * 以前から準備はしていたものの記録するまでは至っていなかった。リザルトに使用するかは置いておいて、スコアに通じるものができました。
  - 炎のアニメーションを追加しました。
  - 外部アセットからエフェクトを一つインポートしました。
  - M5を振るごとに炎が大きくなり、何もしなければ小さくなるようにしました
  - 炎が見にくいサイズまで拡大・縮小されないよう上限と下限をつけました
  - ゲームのロゴを作りました。
      * あまりしっくりきていないので変えるかもしれません
  - 30秒のタイマーが可視化されカウントダウンがわかるようになりました
  <p>温度計をせっかく作ったので活用したいのと、エフェクトを表示させるようにしたいです。データが虚空に消えていきました。重めのアセットをUnityのアセットストアからダウンロードしGitのバージョン管理に突っ込んだところとても重くなり、実質的にフリーズしました。自分が使っているUnityのバージョンでは、UnityHubとEditorでUnityアカウントのサインイン情報が紐づいていないようでアセットをダウンロードするのにもかなり時間を取られました。展示用のスライムを作って現実から逃げていると2時間は経ってました。恐ろしいです。</p></br></br>
  <p>タスクメモ 後で消します</p>

  - 炎の見た目をなんとかする
  - 温度の処理を整える
  - 制限時間の可視化
  - 3カウント（できれば）
  - M5の加速度適応
  - M5のボタン適応
</article></br></br>

<h2>2024/08/01記載</h2>
<article>
  
  - キーボードでアイテムを選ぶと、ゲーム内でカウントしている入力回数がズレる場合があることが判明したので修正しました。
  - リザルト画面で表示するパンのオブジェクトが見つからないときに何も表示されなかったところを、余っている3Dモデルを表示させることでリザルトにパンが必ず表示されるようになりました。
  
</article></br></br>

<h2>2024/07/31記載</h2>
<article>

  - 素材の画像を一枚追加しました。
  <p>スクリプト構成の変更はパンを焼くSceneまで到達できませんでした。</p>
</article></br></br>

<h2>2024/07/30記載</h2>
<article>
  <p>ファイル構造を整えようとしたら苦戦しました。</p>
</article></br></br>

<h2>2024/07/29　(2024/08/01記載)</h2>
<article>

  - UnityにlilToonという外部アセットを導入しました。
    * パンのオブジェクトにかけるシェーダーを変更したことで、かなりリザルト画面の見た目が良くなりました。
  <p>MToonのように輪郭線や影の色などが自由に変更でき、かなり自由度の高いシェーダーでした。UniVRMを別プロジェクトでインポートしてMToonについても触ってみました。</p>
  
</article></br>
