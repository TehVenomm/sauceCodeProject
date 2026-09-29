.class public abstract Lcom/zopim/android/sdk/data/Path;
.super Ljava/util/Observable;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "<T:",
        "Ljava/lang/Object;",
        ">",
        "Ljava/util/Observable;"
    }
.end annotation


# static fields
.field protected static final DEBUG:Z = false

.field private static final LOG_TAG:Ljava/lang/String; = "Path"


# instance fields
.field protected final PARSER:Lcom/zopim/android/sdk/data/Parser;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/zopim/android/sdk/data/Parser<",
            "TT;>;"
        }
    .end annotation
.end field

.field protected mData:Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "TT;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    invoke-direct {p0}, Ljava/util/Observable;-><init>()V

    new-instance v0, Lcom/zopim/android/sdk/data/Parser;

    invoke-direct {v0}, Lcom/zopim/android/sdk/data/Parser;-><init>()V

    iput-object v0, p0, Lcom/zopim/android/sdk/data/Path;->PARSER:Lcom/zopim/android/sdk/data/Parser;

    return-void
.end method


# virtual methods
.method public broadcast()V
    .locals 1

    invoke-virtual {p0}, Lcom/zopim/android/sdk/data/Path;->getData()Ljava/lang/Object;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/data/Path;->broadcast(Ljava/lang/Object;)V

    return-void
.end method

.method protected broadcast(Ljava/lang/Object;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TT;)V"
        }
    .end annotation

    invoke-virtual {p0}, Lcom/zopim/android/sdk/data/Path;->countObservers()I

    move-result v0

    if-lez v0, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/data/Path;->setChanged()V

    invoke-super {p0, p1}, Ljava/util/Observable;->notifyObservers(Ljava/lang/Object;)V

    :cond_0
    return-void
.end method

.method abstract clear()V
.end method

.method protected finalize()V
    .locals 0

    invoke-super {p0}, Ljava/lang/Object;->finalize()V

    return-void
.end method

.method public abstract getData()Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()TT;"
        }
    .end annotation
.end method

.method protected isClearRequired(Ljava/lang/String;)Z
    .locals 1

    if-eqz p1, :cond_1

    const-string v0, "null"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    return p1

    :cond_1
    :goto_0
    const/4 p1, 0x1

    return p1
.end method

.method public final notifyObservers(Ljava/lang/Object;)V
    .locals 2
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    :try_start_0
    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/data/Path;->broadcast(Ljava/lang/Object;)V
    :try_end_0
    .catch Ljava/lang/ClassCastException; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception p1

    sget-object v0, Lcom/zopim/android/sdk/data/Path;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Parametrized object should be of specified type T. Will not notify observers."

    invoke-static {v0, v1, p1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    return-void
.end method

.method abstract update(Ljava/lang/String;)V
.end method
