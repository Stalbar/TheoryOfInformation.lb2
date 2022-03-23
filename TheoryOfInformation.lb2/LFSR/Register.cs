using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TheoryOfInformation.LFSR
{
    /// <summary>
    /// Realization of LFSR
    /// </summary>
    internal class Register
    {
        #region Private Members

        // Powers of our polynomial
        private int[] powers;

        // Current state of LFSR
        private int[] buf;

        // Cells that take part in XOR operation
        private int[] xorCells;
        #endregion

        #region Constructor

        public Register(int[] numbers, string initial)
        {
            // Copy powers from our polynomial
            powers = new int[numbers.Length];
            Array.Copy(numbers, 0, powers, 0, numbers.Length);
            // Set buf length to number of register cells
            buf = new int[powers.Max()];
            // Set initial state of buf and initial key state
            int i = 0;
            for (; i < buf.Length && i < initial.Length; i++)
            {
                buf[buf.Length - 1 - i] = (int)(initial[i] - '0');
            }
            for (; i < buf.Length; i++)
                buf[buf.Length - 1 - i] = 1;
            // Set length of XOR cells
            xorCells = new int[powers.Length];
        }

        #endregion

        #region Public Methods
        // Generate new key bit
        public int GenerateKeyBit()
        {
            // Get keyBit
            int keyBit = buf[buf.Length - 1];
            // Get cells that take part in XOR operation
            for (int i = 0; i < powers.Length; i++)
                xorCells[i] = buf[powers[i] - 1];
            // Shift of register
            for (int i = buf.Length - 1; i > 0; i--)
                buf[i] = buf[i - 1];
            // Generete new first bit
            buf[0] = xorCells[0];
            for (int i = 1; i < xorCells.Length; i++)
                buf[0] ^= xorCells[i];
            return keyBit;
        }
        #endregion
    }
}
