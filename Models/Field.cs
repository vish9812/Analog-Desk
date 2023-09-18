namespace Analog.Models;

public class Field
{
    public string Name { get; set; }
    public string Data { get; set; }
    public bool IsChecked { get; set; }

    // override object.Equals
    public override bool Equals(object obj)
    {
        //
        // See the full list of guidelines at
        //   http://go.microsoft.com/fwlink/?LinkID=85237
        // and also the guidance for operator== at
        //   http://go.microsoft.com/fwlink/?LinkId=85238
        //

        if (obj == null || obj is not Field)
        {
            return false;
        }

        var field = (Field)obj;

        return this.Name.Trim().ToLower() == field.Name.Trim().ToLower();
    }

    // override object.GetHashCode
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}