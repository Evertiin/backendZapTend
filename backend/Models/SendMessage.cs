using System.Collections.Generic;

public class SendMessage
{
    public string Number { get; set; } // Número do destinatário com código do país
    public string Text { get; set; } // Texto da mensagem
    public int? Delay { get; set; } // Opcional: atraso em milissegundos
    public QuotedMessage Quoted { get; set; } // Mensagem citada (opcional)
    public bool? LinkPreview { get; set; } // Opcional: pré-visualização de link
    public bool? MentionsEveryOne { get; set; } // Opcional: menciona todos
    public List<string> Mentioned { get; set; } // Opcional: lista de números mencionados

    public SendMessage()
    {
        Mentioned = new List<string>();
    }
}

public class QuotedMessage
{
    public QuotedKey Key { get; set; } // Chave da mensagem citada
    public QuotedContent Message { get; set; } // Conteúdo da mensagem citada
}

public class QuotedKey
{
    public string Id { get; set; } // ID da mensagem citada
}

public class QuotedContent
{
    public string Conversation { get; set; } // Conteúdo da conversa citada
}
