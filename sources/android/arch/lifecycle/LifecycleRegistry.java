package android.arch.lifecycle;

import android.arch.core.internal.FastSafeIterableMap;
import android.arch.lifecycle.Lifecycle.Event;
import android.arch.lifecycle.Lifecycle.State;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.Map.Entry;

public class LifecycleRegistry extends Lifecycle {
    private int mAddingObserverCounter = 0;
    private boolean mHandlingEvent = false;
    private final LifecycleOwner mLifecycleOwner;
    private boolean mNewEventOccurred = false;
    private FastSafeIterableMap<LifecycleObserver, ObserverWithState> mObserverMap = new FastSafeIterableMap<>();
    private ArrayList<State> mParentStates = new ArrayList<>();
    private State mState;

    static class ObserverWithState {
        GenericLifecycleObserver mLifecycleObserver;
        State mState;

        ObserverWithState(LifecycleObserver lifecycleObserver, State state) {
            this.mLifecycleObserver = Lifecycling.getCallback(lifecycleObserver);
            this.mState = state;
        }

        /* access modifiers changed from: 0000 */
        public void dispatchEvent(LifecycleOwner lifecycleOwner, Event event) {
            State stateAfter = LifecycleRegistry.getStateAfter(event);
            this.mState = LifecycleRegistry.min(this.mState, stateAfter);
            this.mLifecycleObserver.onStateChanged(lifecycleOwner, event);
            this.mState = stateAfter;
        }
    }

    public LifecycleRegistry(@NonNull LifecycleOwner lifecycleOwner) {
        this.mLifecycleOwner = lifecycleOwner;
        this.mState = State.INITIALIZED;
    }

    private void backwardPass() {
        Iterator descendingIterator = this.mObserverMap.descendingIterator();
        while (descendingIterator.hasNext() && !this.mNewEventOccurred) {
            Entry entry = (Entry) descendingIterator.next();
            ObserverWithState observerWithState = (ObserverWithState) entry.getValue();
            while (observerWithState.mState.compareTo(this.mState) > 0 && !this.mNewEventOccurred && this.mObserverMap.contains(entry.getKey())) {
                Event downEvent = downEvent(observerWithState.mState);
                pushParentState(getStateAfter(downEvent));
                observerWithState.dispatchEvent(this.mLifecycleOwner, downEvent);
                popParentState();
            }
        }
    }

    private State calculateTargetState(LifecycleObserver lifecycleObserver) {
        Entry ceil = this.mObserverMap.ceil(lifecycleObserver);
        return min(min(this.mState, ceil != null ? ((ObserverWithState) ceil.getValue()).mState : null), !this.mParentStates.isEmpty() ? (State) this.mParentStates.get(this.mParentStates.size() - 1) : null);
    }

    private static Event downEvent(State state) {
        switch (state) {
            case INITIALIZED:
                throw new IllegalArgumentException();
            case CREATED:
                return Event.ON_DESTROY;
            case STARTED:
                return Event.ON_STOP;
            case RESUMED:
                return Event.ON_PAUSE;
            case DESTROYED:
                throw new IllegalArgumentException();
            default:
                throw new IllegalArgumentException("Unexpected state value " + state);
        }
    }

    private void forwardPass() {
        IteratorWithAdditions iteratorWithAdditions = this.mObserverMap.iteratorWithAdditions();
        while (iteratorWithAdditions.hasNext() && !this.mNewEventOccurred) {
            Entry entry = (Entry) iteratorWithAdditions.next();
            ObserverWithState observerWithState = (ObserverWithState) entry.getValue();
            while (observerWithState.mState.compareTo(this.mState) < 0 && !this.mNewEventOccurred && this.mObserverMap.contains(entry.getKey())) {
                pushParentState(observerWithState.mState);
                observerWithState.dispatchEvent(this.mLifecycleOwner, upEvent(observerWithState.mState));
                popParentState();
            }
        }
    }

    static State getStateAfter(Event event) {
        switch (event) {
            case ON_CREATE:
            case ON_STOP:
                return State.CREATED;
            case ON_START:
            case ON_PAUSE:
                return State.STARTED;
            case ON_RESUME:
                return State.RESUMED;
            case ON_DESTROY:
                return State.DESTROYED;
            default:
                throw new IllegalArgumentException("Unexpected event value " + event);
        }
    }

    private boolean isSynced() {
        if (this.mObserverMap.size() == 0) {
            return true;
        }
        State state = ((ObserverWithState) this.mObserverMap.eldest().getValue()).mState;
        State state2 = ((ObserverWithState) this.mObserverMap.newest().getValue()).mState;
        return state == state2 && this.mState == state2;
    }

    static State min(@NonNull State state, @Nullable State state2) {
        return (state2 == null || state2.compareTo(state) >= 0) ? state : state2;
    }

    private void popParentState() {
        this.mParentStates.remove(this.mParentStates.size() - 1);
    }

    private void pushParentState(State state) {
        this.mParentStates.add(state);
    }

    private void sync() {
        while (!isSynced()) {
            this.mNewEventOccurred = false;
            if (this.mState.compareTo(((ObserverWithState) this.mObserverMap.eldest().getValue()).mState) < 0) {
                backwardPass();
            }
            Entry newest = this.mObserverMap.newest();
            if (!this.mNewEventOccurred && newest != null && this.mState.compareTo(((ObserverWithState) newest.getValue()).mState) > 0) {
                forwardPass();
            }
        }
        this.mNewEventOccurred = false;
    }

    private static Event upEvent(State state) {
        switch (state) {
            case INITIALIZED:
            case DESTROYED:
                return Event.ON_CREATE;
            case CREATED:
                return Event.ON_START;
            case STARTED:
                return Event.ON_RESUME;
            case RESUMED:
                throw new IllegalArgumentException();
            default:
                throw new IllegalArgumentException("Unexpected state value " + state);
        }
    }

    public void addObserver(LifecycleObserver lifecycleObserver) {
        ObserverWithState observerWithState = new ObserverWithState(lifecycleObserver, this.mState == State.DESTROYED ? State.DESTROYED : State.INITIALIZED);
        if (((ObserverWithState) this.mObserverMap.putIfAbsent(lifecycleObserver, observerWithState)) == null) {
            boolean z = this.mAddingObserverCounter != 0 || this.mHandlingEvent;
            State calculateTargetState = calculateTargetState(lifecycleObserver);
            this.mAddingObserverCounter++;
            while (observerWithState.mState.compareTo(calculateTargetState) < 0 && this.mObserverMap.contains(lifecycleObserver)) {
                pushParentState(observerWithState.mState);
                observerWithState.dispatchEvent(this.mLifecycleOwner, upEvent(observerWithState.mState));
                popParentState();
                calculateTargetState = calculateTargetState(lifecycleObserver);
            }
            if (!z) {
                sync();
            }
            this.mAddingObserverCounter--;
        }
    }

    public State getCurrentState() {
        return this.mState;
    }

    public int getObserverCount() {
        return this.mObserverMap.size();
    }

    public void handleLifecycleEvent(Event event) {
        this.mState = getStateAfter(event);
        if (this.mHandlingEvent || this.mAddingObserverCounter != 0) {
            this.mNewEventOccurred = true;
            return;
        }
        this.mHandlingEvent = true;
        sync();
        this.mHandlingEvent = false;
    }

    public void markState(State state) {
        this.mState = state;
    }

    public void removeObserver(LifecycleObserver lifecycleObserver) {
        this.mObserverMap.remove(lifecycleObserver);
    }
}
