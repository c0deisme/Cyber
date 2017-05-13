﻿
namespace Cyber.Items {

    /// <summary>
    /// The <see cref="Drive"/> interface, which contains a grid of indices of items in the <see cref="Drive"/>. Use <see cref="GetItemAt(int, int)"/> to get items in the interface.
    /// </summary>
    public class DriveInterface {

        /// <summary>
        /// Width of the interface.
        /// </summary>
        public const int Width = 8;

        /// <summary>
        /// Minimun height of the interface.
        /// </summary>
        public const int MinHeight = 4;

        private int[,] ItemGrid = new int[4, Width];

        private Drive Drive;

        /// <summary>
        /// Creates a Drive interface for a <see cref="Drive"/>.
        /// </summary>
        /// <param name="drive"></param>
        public DriveInterface(Drive drive) {
            Drive = drive;
        }
        
        /// <summary>
        /// Returns the item at the specified coordinate on the interface. Returns null if invalid or empty coordinate.
        /// </summary>
        /// <param name="x">The x-coordinate</param>
        /// <param name="y">The y-coordinate</param>
        /// <returns>The item or null</returns>
        public Item GetItemAt(int x, int y) {
            if (ItemGrid[y, x] == 0) {
                return null;
            } else {
                return Drive.GetItem(ItemGrid[y, x]);
            }
        }

        /// <summary>
        /// Gets the Width of the interface, or simply <see cref="Width"/>.
        /// </summary>
        /// <returns></returns>
        public int GetWidth() {
            return Width;
        }

        /// <summary>
        /// Gets the current height of the interface
        /// </summary>
        /// <returns></returns>
        public int GetHeight() {
            return ItemGrid.GetLength(1);
        }

        /// <summary>
        /// Updates the height of the interface, adding new rows or deleting old useless ones.
        /// </summary>
        public void UpdateHeight() {
            int RequiredHeight = MinHeight;
            for (int y = MinHeight; y < GetHeight(); y++) {
                for (int x = 0; x < Width; x++) {
                    if (GetItemAt(x, y) == null || (x == Width - 1 && y == GetHeight() - 1)) {
                        RequiredHeight = y;
                    }
                }
            }

            int[,] Temp = new int[RequiredHeight + 1, Width];
            for (int y = 0; y < RequiredHeight - 1; y++) {
                for (int x = 0; x < Width; x++) {
                    if (GetItemAt(x, y) != null) {
                        Temp[y, x] = Drive.GetItems().IndexOf(GetItemAt(x, y));
                    }
                }
            }

            ItemGrid = Temp;
        }

        /// <summary>
        /// Adds a new item to the grid. The idx in the parameter is the idx of the item in the drive.
        /// </summary>
        /// <param name="idx"></param>
        public void AddNewItem(int idx) {
            UpdateHeight();
            for (int y = 0; y < GetHeight(); y++) {
                for (int x = 0; x < Width; x++) {
                    if (GetItemAt(x, y) == null) {
                        ItemGrid[y, x] = idx;
                    }
                }
            }
        }
    }
}
