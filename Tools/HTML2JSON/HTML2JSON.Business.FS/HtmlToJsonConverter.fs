namespace HTML2JSON.Business.FS

type HtmlToJsonConverterFS(html) = 
    member this.Html = new HtmlAgilityPack.HtmlDocument()  { OptionOutputAsXml = true }
    //member this.ToJson with 