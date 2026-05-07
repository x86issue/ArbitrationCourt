
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
    public Claim(Plaintiff plaintiff, Defendant defendant, ClaimContent content): base()
    {
        Plaintiff = plaintiff ?? throw new ArgumentNullException(nameof(plaintiff));
        Defendant = defendant ?? throw new ArgumentNullException(nameof(defendant));

        Content = content;
    }


}