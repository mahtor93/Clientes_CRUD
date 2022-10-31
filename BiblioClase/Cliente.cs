using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioClase;


namespace BiblioClase
{
    public class Cliente
    {
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Sexo Sexo { get; set; }
        public EstadoCivil EstCiv { get; set; }

        public bool Validacion()
        {

            
                string msg = "";
                if (Rut.Length == 0) msg += "\nEl Rut no puede estar vacío";
                if (Nombre.Length == 0) msg += "\nEl Nombre no puede estar vacío";
                if (Apellido.Length == 0) msg += "\nEl Apellido no puede estar vacío";
                if (Sexo == Sexo.nulo) msg += "\nSeleccione un sexo por favor";
                if (EstCiv.Equals(EstadoCivil.Seleccione)) msg += "\nSeleccione un estado civil por favor";
                if (FechaNacimiento.Equals((01 / 01 / 0001))) msg += "\nSeleccione una fecha de nacimiento válida";
                if (msg != "")
                {
                    throw new Exception(msg);
                }

                return true;
            

        }
    }


}
