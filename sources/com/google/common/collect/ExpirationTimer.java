package com.google.common.collect;

import java.util.Timer;

class ExpirationTimer {
    static Timer instance = new Timer(true);

    ExpirationTimer() {
    }
}
