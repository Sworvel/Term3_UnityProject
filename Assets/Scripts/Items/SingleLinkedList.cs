using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SingleLinkedList : MonoBehaviour
{
    public class Node
    {
        public GameObject gameObject;
        public Node next;

        public Node(GameObject newObject)
        {
            gameObject = newObject;
            next = null;
        }
    }

    Node head, tail;
    uint size;

    public SingleLinkedList()
    {
        head = null;
        tail = null;
    }

    ~SingleLinkedList()
    {
        clear();
    }

    public void clear()
    {
        while (!empty())
        {
            popFront();
        }
    }

    public bool empty()
    {
        return size == 0;
    }

    public uint Size()
    {
        return size;
    }

    public GameObject front()
    {
        return head.gameObject;
    }

    public GameObject back()
    {
        return tail.gameObject;
    }

    public void pushFront(GameObject newObject)
    {
        Node newNode = new Node(newObject);
        if (empty())
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            newNode.next = head;
            head = newNode;
        }
        size++;
    }

    public void pushBack(GameObject newObject)
    {
        Node newNode = new Node(newObject);
        if (empty())
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            tail.next = newNode;
            tail = newNode;
        }
        size++;
    }

    public void popFront()
    {
        if (head == tail)
        {
            head = null;
            tail = null;
        }
        else
        {
            head = head.next;
        }
        size--;
    }

    public void popBack()
    {
        Node previousTail = tail;

        if (head == tail)
        {
            head = null;
            tail = null;
        }
        else
        {
            tail = head;
            while (tail.next != previousTail)
            {
                tail = tail.next;
            }
            tail.next = null;
        }
        size--;
    }

    public class iterator
    {
        public Node node;
        public iterator()
        {
            node = null;
        }

        public static bool operator == (iterator lhs, iterator rhs)
        {
            return lhs.node == rhs.node;
        }

        public static bool operator != (iterator lhs, iterator rhs)
        {
            return lhs.node != rhs.node;
        }

        public static iterator operator ++ (iterator it)
        {
            if(it.node.next != null)
            {
                it.node = it.node.next;
                return it;
            }
            return null;
        }
    }

    public Node findNode(GameObject value)
    {
        for(Node node = head; node != null; node = node.next)
        {
            if (node.gameObject == value)
            {
                return node;
            }
        }
        return null;
    }

    public iterator begin()
    {
        iterator newit = new iterator();
        newit.node = head;
        return newit;
    }

    public iterator end()
    {
        return null;
    }

    iterator erase(iterator pos)
    {
        Node target = pos.node;  // save target to be erased

        ++pos;  // advance iterator

        if (target == head)
            popFront();
        else if (target == tail)
            popBack();
        else
        {
            // find the node before target
            Node tmp = head;
            while (tmp.next != target)
                tmp = tmp.next;
            // unlink target node
            tmp.next = target.next;
            // delete target node
            --size;
        }

        return pos; // return advanced iterator
    }

    iterator insert(iterator pos, GameObject value)
	{
        iterator it = new iterator();
		if (pos == begin()) {
			// insert new node before head
			pushFront(value);
            it.node = head;
			return it;
		} else if (pos == end())
    {
        pushBack(value);
                it.node = tail;
        return it;
    }
        else
         {
        // find the node before pos
        Node tmp = head;
        while (tmp.next != pos.node)
            tmp = tmp.next;
    
        // create new node to be inserted
        Node new_node = new Node(value);
    
        // insert new_node between tmp and pos
        tmp.next = new_node;
        new_node.next = pos.node;
    
        ++size;
                it.node = new_node;
        return it;
        }
   }

}
