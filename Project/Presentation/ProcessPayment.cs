
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

            if (choice == "0")
            {
                Console.WriteLine("Payment cancelled.");
                Console.CursorVisible = false;
                return null;
            }

            if (choice == "1")
            {
                if (!EnterCreditCardDetails(logic))
                {
                    Console.CursorVisible = false;
                    return null;
                }

                if (!ConfirmPayment(totalAmount))
                {
                    Console.CursorVisible = false;
                    return null;
                }

                Console.CursorVisible = false;
                return logic.GeneratePaymentNumber();
            }

            if (choice == "2")
            {
                if (!EnterIbanDetails(logic))
                {
                    Console.CursorVisible = false;
                    return null;
                }

                if (!ConfirmPayment(totalAmount))
                {
                    Console.CursorVisible = false;
                    return null;
                }

                Console.CursorVisible = false;
                return logic.GeneratePaymentNumber();
            }

            Console.WriteLine("Invalid option. Please try again.");
        }
    }

    private static bool EnterCreditCardDetails(PaymentsLogic logic)
    {
        Console.WriteLine();
        Console.WriteLine("=== CREDIT CARD ===");
        Console.WriteLine("Use test payment details only.");

        if (!AskName(logic))
        {
            return false;
        }

        while (true)
        {
            Console.WriteLine("Enter card number (13-19 digits, 0 to cancel):");
            string? cardNumber = Console.ReadLine();

            if (cardNumber == "0")
            {
                return false;
            }

            if (cardNumber != null && logic.IsValidCardNumber(cardNumber))
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

            Console.WriteLine("Invalid or expired expiration date.");
        }

        while (true)
        {
            Console.WriteLine("Enter CVC/CVV (3-4 digits, 0 to cancel):");
            string? cvc = Console.ReadLine();

            if (cvc == "0")
            {
                return false;
            }

            if (cvc != null && logic.IsValidCvc(cvc))
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
        Console.WriteLine("=== IBAN ===");
        Console.WriteLine("Use test payment details only.");

        if (!AskName(logic))
        {
            return false;
        }

        while (true)
        {
            Console.WriteLine("Enter Dutch IBAN (e.g. NL91 ABNA 0417 1643 00, 0 to cancel):");
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

    private static bool AskName(PaymentsLogic logic)
    {
        while (true)
        {
            Console.WriteLine("Enter account holder's full name (0 to cancel):");
            string? name = Console.ReadLine();

            if (name == "0")
            {
                return false;
            }

            if (name != null && logic.IsValidName(name))
            {
                return true;
            }

            Console.WriteLine(
                "Enter a valid first and last name with uppercase initials.");
        }
    }

    private static bool ConfirmPayment(double totalAmount)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== CONFIRM PAYMENT ===");
            Console.WriteLine($"Total amount: €{totalAmount:F2}");
            Console.WriteLine();
            Console.WriteLine("Do you want to confirm this payment? (Y/N)");

            string? answer = Console.ReadLine();

            if (answer == "Y" || answer == "y")
            {
                return true;
            }

            if (answer == "N" || answer == "n")
            {
                Console.WriteLine("Payment cancelled.");
                return false;
            }

            Console.WriteLine("Invalid input. Please enter Y or N.");
        }
    }

    public static void ShowPaymentSuccess(string paymentNumber, double totalAmount)
    {
        Console.WriteLine();
        Console.WriteLine("=== PAYMENT SUCCESSFUL ===");
        Console.WriteLine($"Total paid: €{totalAmount:F2}");
        Console.WriteLine($"Payment number: {paymentNumber}");
        Console.WriteLine();
        Console.WriteLine("Your purchase has been completed!");

        Console.CursorVisible = false;
    }
}
