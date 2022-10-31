using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClase
{
    public class ColeccionClientes
    {
        private static List<Cliente> listClient = new List<Cliente>();
        
        public static bool Agregar(Cliente c)
        {
            if (BuscarRut(c.Rut) == null)
            {
                listClient.Add(c);
                return true;
            }
            else
            {
                return false;
            }
               
        }
        public static string Modificar(Cliente c)
        {
            Borrar(c.Rut);
            listClient.Add(c);
            return "Modificado con exito";
            
        }
        public static Cliente BuscarRut(string rut)
        {

            for(int i =0;i<listClient.Count;i++) 
            {
                if (listClient[i].Rut.Equals(rut))
                {
                    return listClient[i];
                }
            }
            return null;
        }
        public static List<Cliente> ListarTodos()
        {
            List<Cliente> list = new List<Cliente>();
            foreach(Cliente item in listClient)
            {
                list.Add(item);
            }
            return list;
        }
        public static bool Borrar(string rut)
        {
            if (BuscarRut(rut) != null)
            {
                Cliente delete = BuscarRut(rut);
                for (int i = 0; i < listClient.Count; i++)
                {
                    if (listClient[i].Rut.Equals(rut))
                    {
                        listClient.Remove(listClient[i]);
                    }

                }
                return true;
            }
            return false;
            
        }
        public static List<Cliente> NombreComienzaPor(string inicial)
        {
            List<Cliente> comienzaPor = new List<Cliente>(); //instancia una lista auxiliar
            foreach(Cliente cliente in listClient) //revisa la lista general de clientes
            {
                if (cliente.Nombre.StartsWith(inicial)) //si encuentra una coincidencia el nombre
                {
                    comienzaPor.Add(cliente); //la añade a la lista auxiliar
                }
            }
            return comienzaPor; //devuelve la lista auxiliar con nombres que cumplieron el requisito
        }
    }
}
