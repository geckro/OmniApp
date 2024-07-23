using OmniApp.Common.Logging;
using System.Xml;
using System.Xml.Linq;

namespace OmniApp.Common.Data;

/// <summary>
///     Helper class to help read and write data to and from XML, for config.
/// </summary>
public class XmlConfigHelper(string configFilePath)
{
    private XDocument LoadConfig()
    {
        if (!File.Exists(configFilePath))
        {
            return new XDocument(new XElement(XName.Get("Preferences")));
        }

        try
        {
            return XDocument.Load(configFilePath);
        }
        catch (XmlException)
        {
            return new XDocument(new XElement(XName.Get("Preferences")));
        }
    }

    public void WriteToConfig(XName key, string value)
    {
        XDocument doc = LoadConfig();
        XElement? element = doc.Root?.Element(key);

        if (element != null)
        {
            element.Value = value;
        }
        else
        {
            doc.Root?.Add(new XElement(key, value));
        }

        try
        {
            doc.Save(configFilePath);
        }
        catch (IOException ex)
        {
            Logger.Error(LogClass.OmniCommon, $"Unable to save config file {ex}");
        }
    }

    public string? ReadFromConfig(XName key)
    {
        XDocument doc = LoadConfig();
        return doc.Root?.Element(key)?.Value;
    }

    public bool KeyExists(XName key)
    {
        XDocument doc = LoadConfig();
        return doc.Root?.Element(key) != null;
    }
}
