using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulador_de_mensajes
{
    public partial class Form1 : Form
    {
        List<Contactos> contactos;//en esta lista se guadaran la informacion del contacto y sus mensajes

        String conversacionActual = "";//aca se va a guardar la conversacion que se elijio ver
        Boolean finConverscaion = false;//este atributo indica si se cerro la conversacion
        public Form1()
        {
            contactos = new List<Contactos>();

            InitializeComponent();
        }

        public void CargarConversacion()//este metodo toma los datos del usuario y los guarda en la lista
        {
            String usu = textBoxUsu.Text;//se toman los datos de los Textbox
            String mail = textBoxMail.Text;
            String msj = textBoxmsj.Text;
            if (!usu.Equals("") && !mail.Equals(""))/*si el usuario y el mail cuentan con texto, se puede
                                                    /*continuar*/
            {
                int bandera = 0;/*solo hay una conversacion por usuario, bandera va a comprobar si el usuario
                                /*se repite*/
                foreach (Contactos contactos in contactos)
                {
                    if (contactos.usuario.Text.Equals(usu) || contactos.email.Text.Equals(mail))
                    {
                        bandera = 1;/*se recorre la lista buscando usarios y mails para ver que no se repitan
                                    /*si se llegan a repetir bandera pasa a ser 1 y no se crea la conversacion*/
                    }
                }
                if (bandera == 0)
                {
                    Contactos conversacion = new Contactos(usu, mail, msj);/*se crea un objeto y se envian los
                                                                           /*datos de los chaeckbox*/
                    contactos.Add(conversacion);//se guarda el objeto en la lista
                    usuConver.Text += conversacion.usuario.Text + "\n";
                    emailConver.Text += conversacion.email.Text + "\n";//se envian los datos a las labels que
                    fechaConver.Text += conversacion.fecha.Text + "\n";//estan en el panel de conversaciones
                    textBoxUsu.Text = "";
                    textBoxMail.Text = "";
                    textBoxmsj.Text = "";
                }
            }
        }

        public void BuscarConver() {

            foreach (Contactos contactos in contactos)
            {
                if (contactos.usuario.Text.Equals(buscartb.Text) || contactos.email.Text.Equals(buscartb.Text))
                {/*se recorre la lista de contactos y si el mail o usuario que se cocloco en el buscartb
                 /* coincide con un usuario o mai en la lista se carga el mensaje gaurdado en la lista y
                 /*se muestra en un label dentro del panel mensajes  */
                    buscartb.Text = "";
                    mensajeslb.Text = contactos.mensaje;
                    btnEnviarUsu.Text = "Enviar" + " (" + contactos.usuario.Text + ")";/*el boton enviar
                                                                                       /*toma el nombre
                                                                                       /*del usuario*/
                    conversacionActual = contactos.usuario.Text;//se guarda la conversacion que se encontro
                    finConverscaion = false;//finConversacion se deja en falso ya que hay una conversacion
                }
            }

        }

        public void MensajePropio() {//este metodo muestra tu mensaje en el chat
            if (!tbMensajes.Text.Equals(""))//si el textbox de mansajes no esta vacio
            {
                String textoTu = "\n" + "Tu: " + tbMensajes.Text + "\n";//se toma mi mensaje del textbox
                foreach (Contactos contactos in contactos)
                {
                    if (contactos.usuario.Text.Equals(conversacionActual))
                    {
                        if (finConverscaion == false)//la conversacion sigue estando abierta, no se finalizo
                        {
                            mensajeslb.Text = contactos.mensaje += textoTu;/*es un solo label acumulativo
                                                                           /*asi que se carga el mensaje
                                                                           /*anterior mas el que yo agregue*/
                            tbMensajes.Text = "";
                        }
                    }
                }
            }
        }

        public void MensajeUsuario() {/*este metodo toma el mensaje enviado por el otro usuario, el codigo es
                                      /*mismo que el anterior metodo*/
            if (!tbMensajes.Text.Equals(""))
            {
                String textoUsu = "\n" + conversacionActual + ": " + tbMensajes.Text + "\n";
                foreach (Contactos contactos in contactos)
                {
                    if (contactos.usuario.Text.Equals(conversacionActual))
                    {
                        if (finConverscaion == false)
                        {
                            mensajeslb.Text = contactos.mensaje += textoUsu;
                            tbMensajes.Text = "";
                        }
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        //aca estan los botones que llaman a los metodos
        private void btnCrearConversacion_Click(object sender, EventArgs e)
        {
            CargarConversacion();
        }

        private void buscarConver_Click(object sender, EventArgs e)
        {
            BuscarConver();
        }

        private void btnEnviarTu_Click(object sender, EventArgs e)
        {
            MensajePropio();
        }

        private void btnEnviarUsu_Click(object sender, EventArgs e)
        {
            MensajeUsuario();
        }

        //los botones cuya funcion es mostrar texto no les puse un metodo propio
        private void btnInfoBuscar_Click(object sender, EventArgs e)
        {
            //lblInformacion va a contener una descripcion de lo que hacen las partes del programa
            lblInformacion.Text = "Al colocar el usuario o email correspondiente a una conversacion," +
            "\n" + "se desplegaran los mensajes de la misma.";
            btnOcualtarInfo.Visible = true;
            //cuenta con un boton para borrar el mensaje, invisible haste que se muestre el mensaje
        }

        private void btnInfoMensajes_Click(object sender, EventArgs e)
        {
            lblInformacion.Text = "En este panel podra ver las conversaciones, y tambien podra" +
            "\n" + "simular el envio de mensajes, tanto por su parte como por la" +
            "\n" + "parte del usuario con el que este simulando conversar.";
            btnOcualtarInfo.Visible = true;
        }

        private void btnImfoCrearConv_Click(object sender, EventArgs e)
        {
            lblInformacion.Text = "Aca podra crear una nueva conversacion tomando los datos del" +
            "\n" + "usuario que inicie una conversacion, las cuales aparecenran en" +
            "\n" + "la ventan conversaciones. Solo puede haber una conversacion por" +
            "\n" + "usuario, si el nombre se repite no se creara una nueva conversacion." +
            "";
            btnOcualtarInfo.Visible = true;
        }

        private void btnCerrarMensajes_Click(object sender, EventArgs e)
        {
            //la conversacion se borra, con lo cual fin de la conversacion se pone en true, hasta que se busque otr
            mensajeslb.Text = "";
            finConverscaion = true;
        }

        private void btnOcualtarInfo_Click(object sender, EventArgs e)
        {
            //la informacion se borra y el boton se oculta
            lblInformacion.Text = "!";
            btnOcualtarInfo.Visible = false;
        }
    }

    //esta es la clase Contacto
    public class Contactos
    {
        public Label usuario;
        public Label email;
        public String mensaje;
        public Label fecha;
        public Contactos(String usu, String em, String msj)
        {
            DateTime dia = DateTime.Today;
            usuario = new Label();
            email = new Label();
            fecha = new Label();
            usuario.Text = usu;
            email.Text = em;
            mensaje = msj;
            fecha.Text = dia.ToShortDateString();
        }
    }
}
