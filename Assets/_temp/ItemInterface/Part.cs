using UnityEngine;
using System;
using System.Collections;

public class Part : IPart
{
	public IWhole whole
	{
		get
		{
			return owner != null ? owner.Target as IWhole : null;
		}
		set
		{
			owner = (value != null) ? new WeakReference(value) : null;
			Check();
		}
	}
	WeakReference owner = null;

	public bool allowed
	{
		get	{return _allowed;}
		set
		{
			if(_allowed == value)
				return;

			_allowed = value;
			Check();
		}
	}
	bool _allowed = true;

	public bool running
	{
		get {return _running;}
		set
		{
			if(_running == value)
				return;

			_running = value;

			if(_running)
			{
				if(!_disAssemble)
				{
					_disAssemble = true;
					Assemble();
				}
				Resume();
			}
			else
			{
				Suspend();
			}
		}
	}
	bool _running = false;

	bool _disAssemble = false;

	public void Check()
	{
		running = (allowed && whole != null && whole.running);
	}

	public virtual void Assemble()
	{

	}

	public virtual void Resume()
	{

	}

	public virtual void Suspend()
	{

	}

	public virtual void Disassemble()
	{

	}
}