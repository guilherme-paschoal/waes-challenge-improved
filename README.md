# WAES Challenge

According to the Requirements, this needs to be an Api that receives a Base64 Encoded JSON string in two "left" and "right" endpoints. 

I assumed that the input will be something like:

`{"jsonKey":"jsonValue"}` > Base64 Encode > `eyJqc29uS2V5IjoianNvblZhbHVlIn0=`

### Requirements

Provide 2 http endpoints that accepts JSON base64 encoded binary data on both endpoints
- <host>/v1/diff/<ID>/left and <host>/v1/diff/<ID>/right
- The provided data needs to be diff-ed and the results shall be available on a third endpoint <host>/v1/diff/<ID>
- The results shall provide the following info in JSON format
    - If equal return that
    - If not of equal size just return that
    - If of same size provide insight in where the diffs are, actual diffs are not needed.
    - So mainly offsets + length in the data
  
## Usage

This project was developed on ASP.NET Core using Visual Studio Community Edition on a MacOS system. 

1. Clone Repository into a folder
2. Then you have two options:
    - Open Solution in Visual Studio and Run
    - On a Terminal/Command Line window, navigate to the **PROJECT** folder (under solution) and execute the command `dotnet run`
3. Open Postman then
4. Execute **POST** on `<server:port>/v1/diff/<id>/left` passing the Base64 Encoded JSON String as the **Request Body** (no headers) and make sure you set the media type to be `application/json`
5. Execute **POST** on `<server:port>/v1/diff/<id>/right` passing the Base64 Encoded JSON String as the **Request Body** (no headers) and make sure you set the media type to be `application/json`
6. Execute **GET** on `<server:port>/v1/diff/<id>` to get the JSON response describing the DIFF according to the Requirements.
7. Keep Calm and Notice that everything is perfect while thinking `This author deserves a job at WAES <3`.

### P.S.

There are some suggestions/ideas of improvement on the code, just search for 'Improve'