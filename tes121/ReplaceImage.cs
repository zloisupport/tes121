using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace tes121
{
    class ReplaceImage
    {
        public void ReplaceInternalImage(WordprocessingDocument document, string oldImagesPlaceholderText, byte[] newImageBytes)
        {
            var imagesToRemove = new List<Drawing>();

            IEnumerable<Drawing> drawings = document.MainDocumentPart.Document.Descendants<Drawing>().ToList();
            foreach (Drawing drawing in drawings)
            {
                DocProperties dpr = drawing.Descendants<DocProperties>().FirstOrDefault();
                if (dpr != null && dpr.Name == oldImagesPlaceholderText)
                {
                    foreach (Blip b in drawing.Descendants<Blip>().ToList())
                    {
                        OpenXmlPart imagePart = document.MainDocumentPart.GetPartById(b.Embed);

                        if (newImageBytes == null)
                        {
                            imagesToRemove.Add(drawing);
                        }
                        else
                        {
                            using (var writer = new BinaryWriter(imagePart.GetStream()))
                            {
                                writer.Write(newImageBytes);
                            }
                        }
                    }
                }

                foreach (var image in imagesToRemove)
                {
                    image.Remove();
                }
            }
        }
    }
}
