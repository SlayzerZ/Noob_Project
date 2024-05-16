/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace StateMachineNS
{
    public class StateMachineEnemy
    {
        private Status? m_currentState;
        private Enemy currentCharacter;

        public void ApplyStateChangement(Status a_newState)
        {
            m_currentState = a_newState;
            m_currentState?.OnEnter(currentCharacter);
            m_currentState.OnRequestChangeState += ApplyStateChangement;

        }

        public void SetCharacter(Enemy target)
        {
            currentCharacter = target;
        }

        public void LeaveStateChagement()
        {
            m_currentState?.OnLeave(currentCharacter);
            m_currentState.OnRequestChangeState -= ApplyStateChangement;
            m_currentState = null;
        }


        public bool isRunning()
        {
            return m_currentState != null;
        }

        public void ProcessUpdate()
        {
            m_currentState?.OnProcess(currentCharacter);
        }
    }

    abstract public class Status
    {
        public Action<Status>? OnRequestChangeState;
        abstract public void OnProcess(Enemy target);

        abstract public void OnEnter(Enemy target);

        abstract public void OnLeave(Enemy target);
    }

    public class Patrol : Status
    {
        public override void OnEnter(Enemy target)
        {
            throw new NotImplementedException();
        }

        public override void OnLeave(Enemy target)
        {
            throw new NotImplementedException();
        }

        public override void OnProcess(Enemy target)
        {
            throw new NotImplementedException();
        }
    }

    public class Chase : Status
    {
        public override void OnEnter(Enemy target)
        {
            throw new NotImplementedException();
        }

        public override void OnLeave(Enemy target)
        {
            throw new NotImplementedException();
        }

        public override void OnProcess(Enemy target)
        {
            throw new NotImplementedException();
        }
    }
}
*/