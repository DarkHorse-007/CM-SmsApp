using MessageApp.Configuration;
using MessageApp.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace MessageApp.ServiceCommunicator
{
    /// <summary>
    /// 
    /// </summary>
    public class CMMessageFormatter : IMessageFormatter
    {
        Dictionary<string, string> Metadata;
        XmlWriterSettings Settings;

        public CMMessageFormatter()
        {
            Metadata = new Dictionary<string, string>();
            Settings = new XmlWriterSettings();
            Settings.Encoding = Encoding.UTF8;
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddMetadata(string key, string value)
        {
            if (!Metadata.ContainsKey(key))
            {
                Metadata.Add(key, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FormatMessage(Message msg)
        {
            MemoryStream stream = new MemoryStream();

            using (XmlWriter writer = XmlWriter.Create(stream, Settings))
            {
                // Compose CM's standard xml structure from Message object.
                writer.WriteStartElement("MESSAGES");

                writer.WriteStartElement("AUTHENTICATION");
                writer.WriteStartElement("PRODUCTTOKEN");
                writer.WriteValue(Metadata[ConfigKeys.ProductToken]);
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteStartElement("MSG");
                writer.WriteStartElement("FROM");
                writer.WriteValue(msg.From);
                writer.WriteEndElement();

                writer.WriteStartElement("TO");
                writer.WriteValue(GetFormattedPhoneNumber(msg));
                writer.WriteEndElement();

                writer.WriteStartElement("DCS");
                writer.WriteValue(msg.DCS);
                writer.WriteEndElement();

                writer.WriteStartElement("BODY");
                writer.WriteValue(msg.Body);
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.Flush();
            }

            return Encoding.UTF8.GetString(stream.GetBuffer());
        }

        private string GetFormattedPhoneNumber(Message msg)
        {
            return string.Format("00{0}{1}", msg.CountryCode, msg.To);
        }
    }
}
