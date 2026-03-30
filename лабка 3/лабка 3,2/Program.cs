using System;
using System.IO;
using System.Text;
using System.Text.Json;

public class PaymentData //допоміжний для збереж і зчит джейсрн
{
    public int Number { get; set; } //властивіть
    public decimal Amount { get; set; }
    public string Type { get; set; } = "";
}
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

    public decimal Amount
    {
        get { return amount; }
    }

    public void ChangeAmount(decimal newAmount)
    {
        if (newAmount >= 0)
            amount = newAmount;
    }

    public bool IsLarge(decimal limit)
    {
        return amount > limit;
    }

    public static int GetCount()
    {
        return count;
    }

    public override string ToString()
    {
        return $"Номер: {number}, Сума: {amount}, Тип: {type}";
    }

    public void SaveToJson(string filePath)
    {
        var data = new PaymentData //тимчасовий обєкт
        {
            Number = number,
            Amount = amount,
            Type = type
        };

        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions //перетворює обєкт дата у джайсон текст
        {
        });
        File.WriteAllText(filePath, json);
    }

    public static Payment LoadFromJson(string filePath) // читає файл і створює з нього обєкт пеймент
    {
        string json = File.ReadAllText(filePath);
        PaymentData? data = JsonSerializer.Deserialize<PaymentData>(json);

        if (data == null)
            throw new Exception("Не вдалося зчитати дані з JSON.");

        return new Payment(data.Number, data.Amount, data.Type); //Створює об’єкт Payment із даних, які були в JSON
    }

 
}

class Program
{
    static void Main()
    {
        try
        {
            Console.OutputEncoding = Encoding.UTF8;

            Payment p1 = new Payment(1, 500m, "Поповнення");
            Console.WriteLine("Початковий об'єкт:");
            Console.WriteLine(p1);

            string path = "payment.json";

            p1.SaveToJson(path);
            Console.WriteLine("\nОб'єкт збережено у JSON файл.");

            Payment p2 = Payment.LoadFromJson(path);
            Console.WriteLine("\nОб'єкт, зчитаний з JSON файлу:");
            Console.WriteLine(p2);

            p2.ChangeAmount(1200m);
            Console.WriteLine("\nПісля зміни суми:");
            Console.WriteLine(p2);

            Console.WriteLine("Великий платіж? " + p2.IsLarge(1000m));
            Console.WriteLine("Кількість платежів: " + Payment.GetCount());
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка:");
            Console.WriteLine(ex.ToString());
        }

        Console.WriteLine("\nНатисни Enter...");
        Console.ReadLine();
    }
}