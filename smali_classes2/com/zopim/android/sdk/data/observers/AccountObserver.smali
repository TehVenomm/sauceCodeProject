.class public abstract Lcom/zopim/android/sdk/data/observers/AccountObserver;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/util/Observer;


# static fields
.field private static final LOG_TAG:Ljava/lang/String; = "AccountObserver"


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public abstract update(Lcom/zopim/android/sdk/model/Account;)V
.end method

.method public final update(Ljava/util/Observable;Ljava/lang/Object;)V
    .locals 2

    instance-of v0, p1, Lcom/zopim/android/sdk/data/LivechatAccountPath;

    if-eqz v0, :cond_1

    instance-of p1, p2, Lcom/zopim/android/sdk/model/Account;

    if-eqz p1, :cond_0

    check-cast p2, Lcom/zopim/android/sdk/model/Account;

    invoke-virtual {p0, p2}, Lcom/zopim/android/sdk/data/observers/AccountObserver;->update(Lcom/zopim/android/sdk/model/Account;)V

    goto :goto_0

    :cond_0
    sget-object p1, Lcom/zopim/android/sdk/data/observers/AccountObserver;->LOG_TAG:Ljava/lang/String;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Unexpected broadcast object "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string p2, " Broadcast object should be of type "

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-class p2, Lcom/zopim/android/sdk/model/Account;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, p2}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_0

    :cond_1
    sget-object p2, Lcom/zopim/android/sdk/data/observers/AccountObserver;->LOG_TAG:Ljava/lang/String;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Unexpected broadcast observable "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string p1, " Observable should be of type "

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-class p1, Lcom/zopim/android/sdk/data/LivechatAccountPath;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p2, p1}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method
