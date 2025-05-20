using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjetoGestaoDeClientes
{
    internal class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>();

        enum Menu { Listar = 1, Adicionar = 2, Remover = 3, Sair = 4 }
        static void Main(string[] args)
        {
           Carregar();

            bool escolheuSair = false;
            while (!escolheuSair)
            {
                Console.WriteLine("===============================================");
                Console.WriteLine("         Sistema de Gestão de Clientes         ");
                Console.WriteLine("===============================================");
                Console.WriteLine("Selecione uma opção: \n1 - Listar Clientes\n2 - Adicionar Cliente\n3 - Remover Cliente\n4 - Fechar sitema");
                Console.WriteLine("=============================");
                Console.Write("Opção Selecionada: ");
                int opcao = int.Parse(Console.ReadLine());
                Menu opcaoSelecionada = (Menu)opcao;

                switch (opcaoSelecionada)
                {
                    case Menu.Listar:
                        Listagem();
                        break;

                    case Menu.Adicionar:
                        Adicionar();
                        break;

                    case Menu.Remover:
                        Remover();
                        break;

                    case Menu.Sair:
                        break;
                }
                Console.Clear();
            }
        }

        static void Adicionar()
        {
            MenuCarregamento();

            Cliente cliente = new Cliente();

            Console.WriteLine("========================================");
            Console.WriteLine("         Menu Adicionar Cliente         ");
            Console.WriteLine("========================================");
            Console.Write("Digite o nome do cliente: ");
            cliente.nome = Console.ReadLine();
            Console.Write("Digire o E-mail do cliente: ");
            cliente.email = Console.ReadLine();
            Console.Write("Digite o CPF do cliente: ");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();

            Console.WriteLine("Cliente adicionado com sucesso, aperte ENTER para retornar ao menu.");
            Console.ReadKey();
        }

        static void Listagem()
        {
            MenuCarregamento();

            Console.WriteLine("=========================================");
            Console.WriteLine("         Menu Listagem De Clientes       ");
            Console.WriteLine("=========================================");

            if(clientes.Count >= 1)
            {
                int id = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {id}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"E-mail: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("=============================");
                    id++;
                }
            }
            else
            {
                Console.WriteLine("Não ha nenhum cliente cadastrado em nosso sistema!");
            }

            Console.WriteLine("Aperte ENTER para retornar ao menu");
            Console.ReadKey(); 
        }

        static void Remover()
        {
            MenuCarregamento();

            bool opcaoInvalida = true;
            while (opcaoInvalida)
            {
                Console.WriteLine("=========================================");
                Console.WriteLine("         Menu Remoção De Clientes       ");
                Console.WriteLine("=========================================");
                Console.WriteLine("Digite o ID do cliente a ser removido");
                int id = int.Parse(Console.ReadLine());

                if (id >= 0 && id < clientes.Count)
                {
                    clientes.RemoveAt(id);
                    Salvar();
                    Console.WriteLine("Cliente Removido com sucesso! Aperte ENTER para retornar ao menu.");
                    Console.ReadKey();
                    opcaoInvalida = false;
                }
                else
                {
                    Console.WriteLine("O ID digitado e invalido, aperte ENTER para tentar novamente");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void Salvar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();

            enconder.Serialize(stream, clientes);

            stream.Close();
        }

        static void Carregar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();

            clientes = (List<Cliente>)enconder.Deserialize(stream);

            stream.Close();
        }

         static void MenuCarregamento()
         {
            Console.Clear();
            int contagem = 3;
            while (contagem >= 1)
            {
                Console.WriteLine($"Carregando, Aguarde {contagem} segundos ...");
                contagem--;
                Thread.Sleep(700);
                Console.Clear();
            }
         }
    }
}
