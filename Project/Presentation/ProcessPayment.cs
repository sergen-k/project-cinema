
using Microsoft.Data.Sqlite;

public static class ProcessPayment
{

    public static string? Start(double totalAmount)
    {
        PaymentsLogic logic = new PaymentsLogic();

        if (totalAmount < 0)
        {
            Console.WriteLine("Invalid payment amount.");
            return null;
        }

        Console.CursorVisible = true;

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== PROCESS PAYMENT ===");
            Console.WriteLine($"Total amount: €{totalAmount:F2}");
            Console.WriteLine();
            Console.WriteLine("1. Credit Card");
            Console.WriteLine("2. IBAN");
            Console.WriteLine("0. Cancel Payment");
            Console.Write("Choose a payment method: ");

            string? choice = Console.ReadLine();

            string method = "";
            bool valid = false;

            if (choice == "0")
            {
                Console.WriteLine("Payment cancelled.");
                Console.CursorVisible = false;
                return null;
            }
            else if (choice == "1")
            {
                method = "Credit Card";
                valid = EnterCreditCardDetails(logic);
            }
            else if (choice == "2")
            {
                method = "IBAN";
                valid = EnterIbanDetails(logic);
            }
            else
            {
                Console.WriteLine("Invalid option.");
                continue;
            }

            if (!valid)
            {
                Console.WriteLine("Payment cancelled.");
                Console.CursorVisible = false;
                return null;
            }

            Console.CursorVisible = false;
            return method;
        }
    }

    public static bool Finish(long reservationId, string method, double totalAmount)
    {
        PaymentsLogic logic = new PaymentsLogic();

        try
        {
            string paymentNumber = logic.SavePayment(
                reservationId, method, totalAmount);

            Console.WriteLine();
            Console.WriteLine("=== PAYMENT SUCCESSFUL ===");
            Console.WriteLine($"Total paid: €{totalAmount:F2}");
            Console.WriteLine($"Payment method: {method}");
            Console.WriteLine("Payment status: Success");
            Console.WriteLine($"Payment number: {paymentNumber}");
            Console.WriteLine();
            Console.WriteLine("Your purchase has been completed!");

            return true;
        }
        catch (Microsoft.Data.Sqlite.SqliteException)
        {
            Console.WriteLine("Payment could not be saved.");
            return false;
        }
    }

    private static bool EnterCreditCardDetails(PaymentsLogic logic)
    {
        Console.WriteLine();
        Console.WriteLine("=== CREDIT CARD PAYMENT ===");
        Console.WriteLine("Use test payment details only.");

        if (AskName(logic) == null)
        {
            return false;
        }

        while (true)
        {
            Console.WriteLine("Enter card number (0 to cancel):");
            string? cardNumber = Console.ReadLine();

            if (cardNumber == "0")
            {
                return false;
            }

            if (logic.IsValidCardNumber(cardNumber))
            {
                break;
            }

            Console.WriteLine("Card number must contain 13-19 digits.");
        }

        while (true)
        {
            Console.WriteLine("Enter expiration date (MM/YY, 0 to cancel):");
            string? expirationDate = Console.ReadLine();

            if (expirationDate == "0")
            {
                return false;
            }

            if (expirationDate != null &&
                logic.IsValidExpirationDate(expirationDate))
            {
                break;
            }

            Console.WriteLine("Invalid or expired date.");
        }

        while (true)
        {
            Console.WriteLine("Enter CVC/CVV (0 to cancel):");
            string? cvc = Console.ReadLine();

            if (cvc == "0")
            {
                return false;
            }

            if (logic.IsValidCvc(cvc))
            {
                break;
            }

            Console.WriteLine("CVC/CVV must contain 3-4 digits.");
        }

        return true;
    }

    private static bool EnterIbanDetails(PaymentsLogic logic)
    {
        Console.WriteLine();
        Console.WriteLine("=== IBAN PAYMENT ===");
        Console.WriteLine("Use test payment details only.");

        if (AskName(logic) == null)
        {
            return false;
        }

        while (true)
        {
            Console.WriteLine("Enter Dutch IBAN (0 to cancel):");
            string? iban = Console.ReadLine();

            if (iban == "0")
            {
                return false;
            }

            if (iban != null && logic.IsValidDutchIban(iban))
            {
                break;
            }

            Console.WriteLine("Invalid Dutch IBAN. Please try again.");
        }

        return true;
    }

    private static string? AskName(PaymentsLogic logic)
    {
        while (true)
        {
            Console.WriteLine("Enter account holder's full name (0 to cancel):");
            string? name = Console.ReadLine();

            if (name == "0")
            {
                return null;
            }

            if (name != null && logic.IsValidName(name))
            {
                return name;
            }

            Console.WriteLine(
                "Enter a valid first and last name, both starting with uppercase letters.");
        }
    }

    private static string? ReadMaskedInput()
    {
        string input = "";

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine();
                return null;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                return input;
            }
            else if (key.Key == ConsoleKey.Backspace)
            {
                if (input.Length > 0)
                {
                    input = input.Substring(0, input.Length - 1);
                    Console.Write("\b \b");
                }
            }
            else if (!char.IsControl(key.KeyChar))
            {
                input += key.KeyChar;
                Console.Write("*");
            }
        }
    }
}
