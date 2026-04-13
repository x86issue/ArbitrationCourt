using AC.Domain.Entities.Entities;
using AC.Domain.ValueObjects;

namespace AC.Domain.Entities;
public class Claim
{
    public ClaimContent Content { get; private set; }

    public Plaintiff Plaintiff { get; private set; } 
    public Defendant Defendant { get; private set; }  


    // МЕТОДЫ 



    // КОНСТРУКТОРЫ
    protected Claim() { }
    public Claim(ClaimContent content, Plaintiff plaintiff, Defendant defendant): base()
    {
        Content = content;
        Plaintiff = plaintiff;
        Defendant = defendant;
    }
}