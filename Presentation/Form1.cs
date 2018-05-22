using BSHP.LoggerManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using DataProvider.Interfaces;
using Memory.Common;

namespace Presentation
{
    public partial class Form1 : Form
    {
        #region Atributos

        ILogger log = BSHP.LoggerManager.LogFactory.GetLogger(typeof(Form1));

        private DataProvider.TManager.Provider _proveedorDatos;
        private ValuesMemory.MemoryValues _valoresMemoria;                    // Valores de la recogida del proveedor de datos

        #endregion

        #region Inicialización

        public Form1()
        {
            InitializeComponent();

            Inicializa();
        }

        private void Inicializa()
        {
            try
            {
                log.Information("#############################################################################");
                log.Information("##               INICIANDO PROCESO REQUEST MANAGER. PRENSAS                ##");
                log.Information("#############################################################################");
                log.Information("");
                log.Information("  Fecha de inicio: {0}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                log.Information("");


                log.Information("Inicializando sistema");

                this._valoresMemoria = new ValuesMemory.MemoryValues();
                
                RecuperaVariablesMemoria(); //TODO: Este método irá dentro del RequestMotor

                this._proveedorDatos = new DataProvider.TManager.Provider(ref _valoresMemoria);
                _proveedorDatos.DataChanged += Proveedor_DataChanged;
            }
            catch (Exception ex)
            {
                log.Error("Inicializa. ", ex);
            }
        }

        /// <summary>
        /// Carga el último estado de las variables al cerrar la aplicación
        /// </summary>
        private void RecuperaVariablesMemoria() //TODO: Este metodo debe ir dentro de RequestMotor
        {
            try
            {
                string rutaAplicacion = AppDomain.CurrentDomain.BaseDirectory;
                string ruta = string.Format(@"{0}temp\memory.xml", rutaAplicacion);

                if (System.IO.File.Exists(ruta))
                {
                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(ValuesMemory.MemoryValues));
                    using (var reader = System.Xml.XmlReader.Create(ruta))
                    {
                        ValuesMemory.MemoryValues memory = (ValuesMemory.MemoryValues)serializer.Deserialize(reader);
                        this._valoresMemoria.LoadValues(memory.GetAll());
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("RecuperaVariablesMemoria. ", ex);
            }
        }

        #endregion

        #region Guardar el estado de las variables en memoria durante los reinicios

        /// <summary>
        /// Almacena las variables internas con los valores de tags y solicitudes
        /// </summary>
        private void PersistirVariablesMemoria() //TODO: Éste metodo debe ir dentro de RequestMotor
        {
            try
            {
                log.Debug("PersistirVariables(). Almacenando los valores de las variables relativas al RequestMotor");

                // Memoria
                log.Debug("Serializando valores de las señales");

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.NewLineOnAttributes = true;

                string rutaAplicacion = AppDomain.CurrentDomain.BaseDirectory;

                var serializer = new System.Xml.Serialization.XmlSerializer(this._valoresMemoria.GetType());
                using (var writer = System.Xml.XmlWriter.Create(
                    string.Format(@"{0}temp\memory.xml", rutaAplicacion),
                    settings))
                {
                    serializer.Serialize(writer, this._valoresMemoria);
                }


                //TODO: Añadir la persistencia de las solicitudes


            }
            catch (Exception ex)
            {
                log.Error("PersistirVariables. ", ex);
            }
        }

        #endregion
        
        #region Eventos de los Controles del Formulario

        private void Form1_Shown(object sender, EventArgs e)
        {
            this._proveedorDatos.Start();
        }

        private void btnGuardarMemoria_Click(object sender, EventArgs e)
        {
            this.PersistirVariablesMemoria();
        }

        #endregion


        #region Evento Cambio de Datos

        private void Proveedor_DataChanged(DataReceivedEventArgs e)
        {
            try
            {
                if (e != null && e.Value != null)
                {
                    TagValue value = e.Value;

                    log.Debug("Cambio de datos. Prensa: [{0}]. Tipo: [{1}]. Fecha: [{3}]. Valor: [{2}]",
                        value.Id_Prensa,
                        value.Type.ToString(),
                        !string.IsNullOrEmpty(value.Value) ? value.Value : "null",
                        value.Date.ToString("dd/MM/yyyy HH:mm:ss"));
                }
            }
            catch (Exception ex)
            {
                log.Error("Proveedor_DataChanged. ", ex);
            }
            
        }

        #endregion
    }
}
