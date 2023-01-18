namespace ProvaAdmissionalCSharpApisul
{
  public class ReadInputFile
  {
    public string folder;
    public string text;
    public bool file;

    public void ReadFile()
    {
      Console.WriteLine("Informe o caminho e o nome do arquivo de entrada");
      folder = Console.ReadLine();

      FileInfo fileInfo= new FileInfo(folder);

      if (fileInfo.Exists)
      {
        if (fileInfo.Extension != ".json")
        {
          Console.WriteLine("A extensão do arquivo não é .json.");
          file = false;
        }
        else
        {
          StreamReader reader = new StreamReader(fileInfo.FullName);
          text = reader.ReadToEnd();
          file = true;
        }
      }
      else
      {
        Console.WriteLine("Arquivo não existe.");
        file = false;
      }
    }
  }
}
