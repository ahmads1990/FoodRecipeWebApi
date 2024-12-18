namespace FoodRecipeWebApi.Helpers;

public static class EmailBodyHelper
{
    public static string GenerateEmailBody(string tempelate, Dictionary<string, string> tempelateModel)
    {
        var tempelatePath = $"{Directory.GetCurrentDirectory()}/Templates/{tempelate}.html";
        var streamReader = new StreamReader(tempelatePath);
        var body = streamReader.ReadToEnd();
        streamReader.Close();
        foreach (var item in tempelateModel)
        {
            body = body.Replace(item.Key, item.Value);
        }
        return body;

    }

}
