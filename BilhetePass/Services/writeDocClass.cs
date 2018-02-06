namespace BilhetePass.Services
{
    public class writeDocClass
    {
        public bool writeInDoc(string filePath, string contextText)
        {
            try
            {                
                System.IO.File.WriteAllText(filePath, contextText);
                return true;
            }
            catch (System.Exception ex)
            {                
                return false;
            }            
        }
    }
}