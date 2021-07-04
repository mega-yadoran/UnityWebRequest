# UnityWebRequest 拡張

- UnityでHTTP通信を簡単に行うための拡張ライブラリです。
- [株式会社ビヨンド](https://beyondjapan.com/)の松山賢勝さんの記事 [UnityWebRequest で簡単 HTTP(POST)通信](https://beyondjapan.com/blog/2020/05/unitywebrequest/) をベースに、GETリクエストに対応して細かい部分を自分好みにカスタマイズしました。
  - 基本的な動作の流れおよび使ってる技術はベースから大きな変化はないため、そちらに関しては上記ブログ記事をご参照ください。
  - ソースコードおよび解説記事の公開にあたって松山さんに問い合わせたところ、快諾いただきました。ありがとうございます！

## 使用にあたって

- 使い方は [こちら](https://qiita.com/mega_yadoran/items/85cf6cd5874f435b3ce0) のQiitaの記事を参照ください。
  - 不明点なども記事にコメントいただければと思います。
- サンプルコードに [天気予報 API（livedoor 天気互換）](https://weather.tsukumijima.net/) 使用しています。登録不要で使えますが、善意で運営されているAPIのため大量のリクエスト発行はお控えください。
