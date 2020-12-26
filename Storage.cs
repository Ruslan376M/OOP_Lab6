namespace Лабораторная_работа__7
{
	public class Storage<T>
	{
		private class Node
		{
			public T obj;
			public Node previous;
			public Node next;
		}

		private int size;
		private Node first;
		private Node last;
		private Node current;
		public Storage()
		{
			size = 0;
		}

		public void add(T obj) // Добавляет объект в хранилище в конец списка
		{
			Node temp = new Node();
			temp.obj = obj;

			size++;

			if (first == null)
			{
				first = temp;
				last = temp;
				current = temp;
			}
			else
			{
				last.next = temp;
				temp.previous = last;
				last = temp;
			}
		}

		public void del() // Удаляет текущий элемент
		{
			if (current != null)
			{
				// Переназначение "указателей" соседних элементов
				if (current.previous != null)
					current.previous.next = current.next;
				if (current.next != null)
					current.next.previous = current.previous;

				// Перевод current на следующий или предыдущий элемент
				Node oldCurrent = current;

				if (current.next != null)
					current = current.next;
				else if (current.previous != null)
					current = current.previous;
				else
					current = null;

				// Смена "указателей" first и last, если current был им равен
				if (oldCurrent == first)
					first = current;
				if (oldCurrent == last)
					last = current;

				// Коррекция размера списка
				size--;
			}
		}

		public void previous() // Переносит current на предыдущий элемент в списке, если предыдущий элемент существует
		{
			if (current != null)
				if (current.previous != null)
					current = current.previous;
		}

		public void next() // Переносит current на следующий элемент в списке
		{
			if (current != null)
				current = current.next;
		}

		public bool check(T obj) // Проверяет наличие объекта хранилище, не изменяя current
		{
			Node buffer = first;
			for (int i = 0; i < size; i++, buffer = buffer.next)
				if (buffer.obj.Equals(obj))
					return true;
			return false;
		}

		public bool checkAndSetCurrent(T obj) // Проверяет наличие объекта в хранилище и устанавливает current на этот объект
		{
			Node buffer = first;
			for (int i = 0; i < size; i++, buffer = buffer.next)
				if (buffer.obj.Equals(obj))
				{
					current = buffer;
					return true;
				}
			return false;
		}

		public int getSize()
		{
			return size;
		}

		public T getFirst() // Возвращает первый объект в списке
		{
			return first.obj;
		}

		public T getLast() // Возвращает последний объект в списке
		{
			return last.obj;
		}

		public ref T getCurrent() // Возвращает текущий объект
		{
			return ref current.obj;
		}

		public void setFirst() // Устанавливает current на начало списка
		{
			current = first;
		}

		public void setLast() // Устанавливает current на конец списка
		{
			current = last;
		}

		public bool eol() // Если current = null, return true
		{
			if (current == null)
				return true;
			return false;
		}
	}
}
