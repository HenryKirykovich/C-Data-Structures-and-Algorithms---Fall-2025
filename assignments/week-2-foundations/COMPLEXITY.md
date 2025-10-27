Structure	Operation	        Big-O (Avg)	One-sentence rationale
Array	Access by index	O(1)	Direct indexing allows constant-time access.
Array	Search (unsorted)	O(N)	Must scan elements sequentially to find a value.
List<T>	Add at end	O(1) amortized	Appending is usually fast, with occasional resizing.
List<T>	Insert at index	O(N)	Elements after the index must be shifted.
Stack<T>	Push / Pop / Peek	O(1)	Operations occur at the top only, no shifting required.
Queue<T>	Enqueue / Dequeue / Peek	O(1)	Operations occur at front or back without moving elements.
Dictionary<K,V>	Add / Lookup / Remove	O(1)	Hash table provides constant-time average operations.
HashSet<T>	Add / Contains / Remove	O(1)	Hash-based structure allows fast membership checks.