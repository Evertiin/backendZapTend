namespace APIWhatssApp.Models
{
    public record ReceiveFlowiseMessage
    (
    
    string text,               
    string question,           
    string chatId,             
    string chatMessageId,      
    bool isStreamValid,        
    string sessionId,          
    string memoryType          
    );
}
