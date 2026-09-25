.class public interface abstract Lcom/helpshift/logger/ILogger;
.super Ljava/lang/Object;
.source "ILogger.java"


# static fields
.field public static final DEBUG:I = 0x2

.field public static final ERROR:I = 0x8

.field public static final FATAL:I = 0x10

.field public static final WARN:I = 0x4


# virtual methods
.method public abstract d(Ljava/lang/String;Ljava/lang/String;)V
.end method

.method public varargs abstract d(Ljava/lang/String;Ljava/lang/String;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
.end method

.method public abstract d(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;)V
.end method

.method public varargs abstract d(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
.end method

.method public abstract deleteAllCachedLogs()V
.end method

.method public abstract e(Ljava/lang/String;Ljava/lang/String;)V
.end method

.method public varargs abstract e(Ljava/lang/String;Ljava/lang/String;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
.end method

.method public abstract e(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;)V
.end method

.method public varargs abstract e(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
.end method

.method public abstract enableLogging(ZZ)V
.end method

.method public abstract f(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;)V
.end method

.method public varargs abstract f(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
.end method

.method public abstract getAll()Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/logger/model/LogModel;",
            ">;"
        }
    .end annotation
.end method

.method public abstract getCount(I)I
.end method

.method public abstract setLoggingLevel(I)V
.end method

.method public abstract setTimestampDelta(J)V
.end method

.method public abstract w(Ljava/lang/String;Ljava/lang/String;)V
.end method

.method public varargs abstract w(Ljava/lang/String;Ljava/lang/String;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
.end method

.method public abstract w(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;)V
.end method

.method public varargs abstract w(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
.end method
