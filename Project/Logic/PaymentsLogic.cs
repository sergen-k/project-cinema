
public class PaymentsLogic
{
    private Random _random = new Random();

    public bool IsValidName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        int wordCount = 0;
        bool newWord = true;
        char lastInitial = ' ';

        foreach (char character in name)
        {
            if (character == ' ')
            {
                newWord = true;
                continue;
            }

            if (!char.IsLetter(character))
            {
                return false;
            }

            if (newWord)
            {
                wordCount++;

                if (wordCount == 1 && !char.IsUpper(character))
                {
                    return false;
                }

                lastInitial = character;
                newWord = false;
            }
        }

        return wordCount >= 2 && char.IsUpper(lastInitial);
    }

    private bool IsOnlyDigits(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        foreach (char character in value)
        {
            if (character < '0' || character > '9')
            {
                return false;
            }
        }

        return true;
    }

    public bool IsValidCardNumber(string cardNumber)
    {
        if (string.IsNullOrEmpty(cardNumber))
        {
            return false;
        }

        if (cardNumber.Length < 13 || cardNumber.Length > 19)
        {
            return false;
        }

        return IsOnlyDigits(cardNumber);
    }

    public bool IsValidExpirationDate(string expirationDate)
    {
        if (string.IsNullOrEmpty(expirationDate))
        {
            return false;
        }

        if (expirationDate.Length != 5 || expirationDate[2] != '/')
        {
            return false;
        }

        string monthText = expirationDate.Substring(0, 2);
        string yearText = expirationDate.Substring(3, 2);

        if (!IsOnlyDigits(monthText) || !IsOnlyDigits(yearText))
        {
            return false;
        }

        int month = int.Parse(monthText);
        int year = 2000 + int.Parse(yearText);

        if (month < 1 || month > 12)
        {
            return false;
        }

        DateTime today = DateTime.Today;

        if (year < today.Year)
        {
            return false;
        }

        if (year == today.Year && month < today.Month)
        {
            return false;
        }

        return true;
    }

    public bool IsValidCvc(string cvc)
    {
        if (string.IsNullOrEmpty(cvc))
        {
            return false;
        }

        if (cvc.Length != 3 && cvc.Length != 4)
        {
            return false;
        }

        return IsOnlyDigits(cvc);
    }

    public bool IsValidDutchIban(string iban)
    {
        if (string.IsNullOrWhiteSpace(iban))
        {
            return false;
        }

        string cleanIban = "";

        foreach (char character in iban)
        {
            if (character != ' ')
            {
                cleanIban += char.ToUpper(character);
            }
        }

        if (cleanIban.Length != 18)
        {
            return false;
        }

        if (cleanIban.Substring(0, 2) != "NL")
        {
            return false;
        }

        for (int i = 2; i < 4; i++)
        {
            if (cleanIban[i] < '0' || cleanIban[i] > '9')
            {
                return false;
            }
        }

        for (int i = 4; i < 8; i++)
        {
            if (cleanIban[i] < 'A' || cleanIban[i] > 'Z')
            {
                return false;
            }
        }

        for (int i = 8; i < 18; i++)
        {
            if (cleanIban[i] < '0' || cleanIban[i] > '9')
            {
                return false;
            }
        }

        string rearranged =
            cleanIban.Substring(4) + cleanIban.Substring(0, 4);

        int remainder = 0;

        foreach (char character in rearranged)
        {
            if (character >= '0' && character <= '9')
            {
                int digit = character - '0';
                remainder = (remainder * 10 + digit) % 97;
            }
            else
            {
                int number = character - 'A' + 10;
                remainder = (remainder * 100 + number) % 97;
            }
        }

        return remainder == 1;
    }

    public string GeneratePaymentNumber()
    {
        int randomNumber = _random.Next(10000000, 100000000);

        return "PAY-" + randomNumber;
    }
}
