.class public Lcom/helpshift/common/util/HSObservableList;
.super Ljava/util/ArrayList;
.source "HSObservableList.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "<T:",
        "Ljava/lang/Object;",
        ">",
        "Ljava/util/ArrayList<",
        "TT;>;"
    }
.end annotation


# instance fields
.field private observer:Lcom/helpshift/common/util/HSListObserver;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/helpshift/common/util/HSListObserver<",
            "TT;>;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 17
    invoke-direct {p0}, Ljava/util/ArrayList;-><init>()V

    return-void
.end method

.method public constructor <init>(Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "TT;>;)V"
        }
    .end annotation

    .line 21
    invoke-direct {p0, p1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    return-void
.end method


# virtual methods
.method public add(Ljava/lang/Object;)Z
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TT;)Z"
        }
    .end annotation

    .line 30
    invoke-super {p0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 32
    iget-object v1, p0, Lcom/helpshift/common/util/HSObservableList;->observer:Lcom/helpshift/common/util/HSListObserver;

    if-eqz v1, :cond_0

    .line 33
    iget-object v1, p0, Lcom/helpshift/common/util/HSObservableList;->observer:Lcom/helpshift/common/util/HSListObserver;

    invoke-interface {v1, p1}, Lcom/helpshift/common/util/HSListObserver;->add(Ljava/lang/Object;)V

    :cond_0
    return v0
.end method

.method public addAll(Ljava/util/Collection;)Z
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "+TT;>;)Z"
        }
    .end annotation

    .line 41
    invoke-super {p0, p1}, Ljava/util/ArrayList;->addAll(Ljava/util/Collection;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 43
    iget-object v1, p0, Lcom/helpshift/common/util/HSObservableList;->observer:Lcom/helpshift/common/util/HSListObserver;

    if-eqz v1, :cond_0

    .line 44
    iget-object v1, p0, Lcom/helpshift/common/util/HSObservableList;->observer:Lcom/helpshift/common/util/HSListObserver;

    invoke-interface {v1, p1}, Lcom/helpshift/common/util/HSListObserver;->addAll(Ljava/util/Collection;)V

    :cond_0
    return v0
.end method

.method public prependItems(Ljava/util/Collection;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "+TT;>;)V"
        }
    .end annotation

    const/4 v0, 0x0

    .line 52
    invoke-super {p0, v0, p1}, Ljava/util/ArrayList;->addAll(ILjava/util/Collection;)Z

    return-void
.end method

.method public setAndNotifyObserver(ILjava/lang/Object;)Ljava/lang/Object;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(ITT;)TT;"
        }
    .end annotation

    .line 57
    invoke-super {p0, p1, p2}, Ljava/util/ArrayList;->set(ILjava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 58
    iget-object v0, p0, Lcom/helpshift/common/util/HSObservableList;->observer:Lcom/helpshift/common/util/HSListObserver;

    if-eqz v0, :cond_0

    .line 59
    iget-object v0, p0, Lcom/helpshift/common/util/HSObservableList;->observer:Lcom/helpshift/common/util/HSListObserver;

    invoke-interface {v0, p2}, Lcom/helpshift/common/util/HSListObserver;->update(Ljava/lang/Object;)V

    :cond_0
    return-object p1
.end method

.method public setObserver(Lcom/helpshift/common/util/HSListObserver;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/common/util/HSListObserver<",
            "TT;>;)V"
        }
    .end annotation

    .line 25
    iput-object p1, p0, Lcom/helpshift/common/util/HSObservableList;->observer:Lcom/helpshift/common/util/HSListObserver;

    return-void
.end method
