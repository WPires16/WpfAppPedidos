using System.Linq;

namespace WpfAppPedidos.Utils
{
    public static class ValidadorCPF
    {
        public static bool Validar(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return false;

            if (new string(cpf[0], 11) == cpf)
                return false;

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (cpf[i] - '0') * (10 - i);
            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;
            if (cpf[9] - '0' != digito1)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (cpf[i] - '0') * (11 - i);
            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return cpf[10] - '0' == digito2;
        }
    }
}