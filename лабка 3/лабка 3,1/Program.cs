using System;

class Payment
{
    private readonly int number;   
    private decimal amount;        
    private string type;           

    private static int count = 0;  

    public Payment(int number, decimal amount, string type)
    {
        this.number = number;
        this.amount = amount >= 0 ? amount : 0;
        this.type = type;
        count++;
    }

    public Payment(int number, string type) : this(number, 0, type)
    {
    }

    public decimal Amount //властивість
    {
        get { return amount; }
    }

    public void ChangeAmount(decimal newAmount)  // метод
    {
        if (newAmount >= 0)
            amount = newAmount;
    }

    public bool IsLarge(decimal limit) //метод 
    {
        return amount > limit;
    }

    public static int GetCount() //статичний метод(статичний типу належить всьому класу)
    {
        return count;
    }

    public override string ToString() //метод (оверрайд це перевизначення готового методу , типу переписуємо під себе)
    {
        return $"Номер: {number}, Сума: {amount}, Тип: {type}";
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Payment p1 = new Payment(1, 500m, "Поповнення");
        Payment p2 = new Payment(2, "Переказ");

        Console.WriteLine(p1);
        Console.WriteLine(p2);

        p2.ChangeAmount(1200m);

        Console.WriteLine("Після зміни:");
        Console.WriteLine(p2);

        Console.WriteLine("Великий платіж? " + p2.IsLarge(1000m));

        Console.WriteLine("Кількість платежів: " + Payment.GetCount());
    }
}