using System.Collections;

namespace Wrox.ProCSharp.Generics
{
  public class LinkedList : IEnumerable
  {
    public LinkedListNode First { get; private set; }
    public LinkedListNode Last { get; private set; }

    public LinkedListNode AddLast(object node)
    {
      var newNode = new LinkedListNode(node);
      if (First == null)
      {
        First = newNode;
        Last = First;
      }
      else
      {
                LinkedListNode previous = Last;
                Last.Next = newNode;
                Last = newNode;
                Last.Prev = previous;
      }
      return newNode;
    }

    public IEnumerator GetEnumerator()
    {
      LinkedListNode current = First;
      while (current != null)
      {
        yield return current.Value;
        current = current.Next;
      }
    }
  }

}
