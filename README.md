
# Portfolio Maker
These tool gets data's from Google Play Market and send's it to your Notion table.
**NOTE** : It only works with text like blocks in notion (Title and Description) for now!
![play-store-view](https://i.hizliresim.com/cbsa6t3.jpg)
![notion-view](https://i.hizliresim.com/qyx15xl.jpg)
## Installation

1. Installed dependencies in project.
-- [HtmlAgilityPack](https://www.nuget.org/packages/HtmlAgilityPack/)
-- [RestSharp](https://www.nuget.org/packages/RestSharp/106.11.8-alpha.0.14)
2. Define your [authorization token](https://www.notion.so/my-integrations) and [database ID](https://developers.notion.com/docs) to variables in curly braces
    ```c
    private static string databaseId = "{DATABASE_ID}";
    private static string token = "{AUTHORIZATION_TOKEN_ID}";
    ```
    For production environments...
3. You have to get URL of game in Google Play Store 
    ```sh
    https://play.google.com/store/apps/details?id=com.Garawell.BridgeRace
    ```

## License
MIT License

Copyright (c) 2021 

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
