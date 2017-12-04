using System;
using System.Text;
using Xunit;
using WaesApi.Utils;

namespace WaesApi.Unit.Tests
{
    public class DecodingTest
    {

        [Fact]
        public void DecodesBase64String() {
            var text = "Hi this is a string to encode";
            var toEncode = Encoding.Default.GetBytes(text);
            var encoded = Convert.ToBase64String(toEncode);
            var decoded = DecodingHelper.Decode(encoded);
            Assert.Equal(text, decoded);
        }

        [Fact]
        public void DecodesBase64EscapedJsonString()
        {
            // Breaking this string into lines gave me headache. How about not breaking it? 
            var text = @"[  {    ""postId"": 1,    ""id"": 1,    ""name"": ""id labore ex et quam laborum"",    ""email"": ""Eliseo@gardner.biz"",    ""body"": ""laudantium enim quasi est quidem magnam voluptate ipsam eosntempora quo necessitatibusndolor quam autem quasinreiciendis et nam sapiente accusantium""  },  {    ""postId"": 1,    ""id"": 2,    ""name"": ""quo vero reiciendis velit similique earum"",    ""email"": ""Jayne_Kuhic@sydney.com"",    ""body"": ""est natus enim nihil est dolore omnis voluptatem numquamnet omnis occaecati quod ullam atnvoluptatem error expedita pariaturnnihil sint nostrum voluptatem reiciendis et""  },  {    ""postId"": 1,    ""id"": 3,    ""name"": ""odio adipisci rerum aut animi"",    ""email"": ""Nikita@garfield.biz"",    ""body"": ""quia molestiae reprehenderit quasi aspernaturnaut expedita occaecati aliquam eveniet laudantiumnomnis quibusdam delectus saepe quia accusamus maiores nam estncum et ducimus et vero voluptates excepturi deleniti ratione""  },  {    ""postId"": 1,    ""id"": 4,    ""name"": ""alias odio sit"",    ""email"": ""Lew@alysha.tv"",    ""body"": ""non et atquenoccaecati deserunt quas accusantium unde odit nobis qui voluptatemnquia voluptas consequuntur itaque dolornet qui rerum deleniti ut occaecati""  },  {    ""postId"": 1,    ""id"": 5,    ""name"": ""vero eaque aliquid doloribus et culpa"",    ""email"": ""Hayden@althea.biz"",    ""body"": ""harum non quasi et rationentempore iure ex voluptates in rationenharum architecto fugit inventore cupiditatenvoluptates magni quo et""  }]";
            var encoded = @"Ww0KICB7DQogICAgInBvc3RJZCI6IDEsDQogICAgImlkIjogMSwNCiAgICAibmFtZSI6ICJpZCBsYWJvcmUgZXggZXQgcXVhbSBsYWJvcnVtIiwNCiAgICAiZW1haWwiOiA
                        iRWxpc2VvQGdhcmRuZXIuYml6IiwNCiAgICAiYm9keSI6ICJsYXVkYW50aXVtIGVuaW0gcXVhc2kgZXN0IHF1aWRlbSBtYWduYW0gdm9sdXB0YXRlIGlwc2FtIGVvc1xudGVtcG9yYSBxdW8gbmVjZXNzaX
                        RhdGlidXNcbmRvbG9yIHF1YW0gYXV0ZW0gcXVhc2lcbnJlaWNpZW5kaXMgZXQgbmFtIHNhcGllbnRlIGFjY3VzYW50aXVtIg0KICB9LA0KICB7DQogICAgInBvc3RJZCI6IDEsDQogICAgImlkIjogMiwNC
                        iAgICAibmFtZSI6ICJxdW8gdmVybyByZWljaWVuZGlzIHZlbGl0IHNpbWlsaXF1ZSBlYXJ1bSIsDQogICAgImVtYWlsIjogIkpheW5lX0t1aGljQHN5ZG5leS5jb20iLA0KICAgICJib2R5IjogImVzdCBu
                        YXR1cyBlbmltIG5paGlsIGVzdCBkb2xvcmUgb21uaXMgdm9sdXB0YXRlbSBudW1xdWFtXG5ldCBvbW5pcyBvY2NhZWNhdGkgcXVvZCB1bGxhbSBhdFxudm9sdXB0YXRlbSBlcnJvciBleHBlZGl0YSBwYXJp
                        YXR1clxubmloaWwgc2ludCBub3N0cnVtIHZvbHVwdGF0ZW0gcmVpY2llbmRpcyBldCINCiAgfSwNCiAgew0KICAgICJwb3N0SWQiOiAxLA0KICAgICJpZCI6IDMsDQogICAgIm5hbWUiOiAib2RpbyBhZGlw
                        aXNjaSByZXJ1bSBhdXQgYW5pbWkiLA0KICAgICJlbWFpbCI6ICJOaWtpdGFAZ2FyZmllbGQuYml6IiwNCiAgICAiYm9keSI6ICJxdWlhIG1vbGVzdGlhZSByZXByZWhlbmRlcml0IHF1YXNpIGFzcGVybmF0
                        dXJcbmF1dCBleHBlZGl0YSBvY2NhZWNhdGkgYWxpcXVhbSBldmVuaWV0IGxhdWRhbnRpdW1cbm9tbmlzIHF1aWJ1c2RhbSBkZWxlY3R1cyBzYWVwZSBxdWlhIGFjY3VzYW11cyBtYWlvcmVzIG5hbSBlc3Rcb
                        mN1bSBldCBkdWNpbXVzIGV0IHZlcm8gdm9sdXB0YXRlcyBleGNlcHR1cmkgZGVsZW5pdGkgcmF0aW9uZSINCiAgfSwNCiAgew0KICAgICJwb3N0SWQiOiAxLA0KICAgICJpZCI6IDQsDQogICAgIm5hbWUiOi
                        AiYWxpYXMgb2RpbyBzaXQiLA0KICAgICJlbWFpbCI6ICJMZXdAYWx5c2hhLnR2IiwNCiAgICAiYm9keSI6ICJub24gZXQgYXRxdWVcbm9jY2FlY2F0aSBkZXNlcnVudCBxdWFzIGFjY3VzYW50aXVtIHVuZGU
                        gb2RpdCBub2JpcyBxdWkgdm9sdXB0YXRlbVxucXVpYSB2b2x1cHRhcyBjb25zZXF1dW50dXIgaXRhcXVlIGRvbG9yXG5ldCBxdWkgcmVydW0gZGVsZW5pdGkgdXQgb2NjYWVjYXRpIg0KICB9LA0KICB7DQog
                        ICAgInBvc3RJZCI6IDEsDQogICAgImlkIjogNSwNCiAgICAibmFtZSI6ICJ2ZXJvIGVhcXVlIGFsaXF1aWQgZG9sb3JpYnVzIGV0IGN1bHBhIiwNCiAgICAiZW1haWwiOiAiSGF5ZGVuQGFsdGhlYS5iaXoiL
                        A0KICAgICJib2R5IjogImhhcnVtIG5vbiBxdWFzaSBldCByYXRpb25lXG50ZW1wb3JlIGl1cmUgZXggdm9sdXB0YXRlcyBpbiByYXRpb25lXG5oYXJ1bSBhcmNoaXRlY3RvIGZ1Z2l0IGludmVudG9yZSBjd
                        XBpZGl0YXRlXG52b2x1cHRhdGVzIG1hZ25pIHF1byBldCINCiAgfQ0KXQ==";

            var decoded = DecodingHelper.Decode(encoded);
            Assert.Equal(text, decoded);
        }

        [Fact]
        public void BreakIfInputIsOfInvalidFormat()
        {
            Assert.Throws<FormatException>(() => { DecodingHelper.Decode("asd"); });
        }

        [Fact]
        public void BreakIfInputIsEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => { DecodingHelper.Decode(""); });
        }

        [Fact]
        public void BreakIfInputIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => { DecodingHelper.Decode(null); });
        }

        [Fact]
        public void BreakIfInputIsOnlyWhiteSpaces()
        {
            Assert.Throws<ArgumentNullException>(() => { DecodingHelper.Decode("   "); });
        }
    }
}
