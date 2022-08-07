// --- by Sebastian Lague ---

namespace ChessV2
{
    public unsafe class PieceList
    {

        // Indices of squares occupied by given piece type (only elements up to Count are valid, the rest are unused/garbage)
        private int[] occupiedSquares;
        // Map to go from index of a square, to the index in the occupiedSquares array where that square is stored
        private int[] map;
        private int* mapPtr = (int*)IntPtr.Zero;

        /// <summary>
        /// the index of -1 will have the pieceNumber
        /// </summary>
        public int* occupiedPtr;

        public PieceList(int maxPieceCount = 16)
        {
            maxPieceCount++;
            occupiedSquares = new int[maxPieceCount + 1];
            map = new int[64];
            fixed (int* ptr = &map[1])
                mapPtr = ptr;
            fixed (int* ptr = &occupiedSquares[1])
                occupiedPtr = ptr;
            occupiedPtr[-1] = 0;
        }
        public void AddPieceAtSquare(int square)
        {
            occupiedPtr[occupiedPtr[-1]] = square;
            mapPtr[square] = occupiedPtr[-1];
            occupiedPtr[-1]++;
        }

        public void RemovePieceAtSquare(int square)
        {
            int pieceIndex = mapPtr[square]; // get the index of this element in the occupiedSquares array
            occupiedPtr[pieceIndex] = occupiedPtr[occupiedPtr[-1] - 1]; // move last element in array to the place of the removed element
            mapPtr[occupiedPtr[pieceIndex]] = pieceIndex; // update map to point to the moved element's new location in the array
            occupiedPtr[-1]--;
        }

        public void MovePiece(int startSquare, int targetSquare)
        {
            int pieceIndex = mapPtr[startSquare]; // get the index of this element in the occupiedSquares array
            occupiedPtr[pieceIndex] = targetSquare;
            mapPtr[targetSquare] = pieceIndex;
        }
    }
}