using System;
using System.Collections.Generic;
using System.Text;

namespace APB.Framework.Security.Enums
{

    /// <summary>
    /// Tipos de Security Objects reconhecidos pelo sistema
    /// </summary>
    [Serializable()]
    public enum ObjectTypes
    {
        /// <summary>
        /// Módulo ou Grupo Raiz
        /// </summary>
        Module = 1,
        /// <summary>
        /// Menu (Nivel superior de um menu)
        /// </summary>
        Menu = 2,
        /// <summary>
        /// WebForm
        /// </summary>
        Form = 3,
        /// <summary>
        /// Botoes, labels, controles filhos de um webform ou menu
        /// </summary>
        Item = 4,
        /// <summary>
        /// Lista tipo dropdown, listbox ou derivados de listcontrol
        /// </summary>
        List = 5        
    }

}
