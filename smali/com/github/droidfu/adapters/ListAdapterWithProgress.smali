.class public abstract Lcom/github/droidfu/adapters/ListAdapterWithProgress;
.super Landroid/widget/BaseAdapter;
.source "ListAdapterWithProgress.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "<T:",
        "Ljava/lang/Object;",
        ">",
        "Landroid/widget/BaseAdapter;"
    }
.end annotation


# instance fields
.field private data:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList<",
            "TT;>;"
        }
    .end annotation
.end field

.field private isLoadingData:Z

.field private listView:Landroid/widget/AbsListView;

.field private progressView:Landroid/view/View;


# direct methods
.method public constructor <init>(Landroid/app/Activity;Landroid/widget/AbsListView;I)V
    .locals 1

    .line 47
    invoke-direct {p0}, Landroid/widget/BaseAdapter;-><init>()V

    .line 35
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->data:Ljava/util/ArrayList;

    .line 49
    iput-object p2, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->listView:Landroid/widget/AbsListView;

    .line 50
    invoke-virtual {p1}, Landroid/app/Activity;->getLayoutInflater()Landroid/view/LayoutInflater;

    move-result-object p1

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->progressView:Landroid/view/View;

    return-void
.end method

.method public constructor <init>(Landroid/app/ExpandableListActivity;I)V
    .locals 1

    .line 44
    invoke-virtual {p1}, Landroid/app/ExpandableListActivity;->getExpandableListView()Landroid/widget/ExpandableListView;

    move-result-object v0

    invoke-direct {p0, p1, v0, p2}, Lcom/github/droidfu/adapters/ListAdapterWithProgress;-><init>(Landroid/app/Activity;Landroid/widget/AbsListView;I)V

    return-void
.end method

.method public constructor <init>(Landroid/app/ListActivity;I)V
    .locals 1

    .line 40
    invoke-virtual {p1}, Landroid/app/ListActivity;->getListView()Landroid/widget/ListView;

    move-result-object v0

    invoke-direct {p0, p1, v0, p2}, Lcom/github/droidfu/adapters/ListAdapterWithProgress;-><init>(Landroid/app/Activity;Landroid/widget/AbsListView;I)V

    return-void
.end method

.method private isPositionOfProgressElement(I)Z
    .locals 1

    .line 160
    iget-boolean v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->isLoadingData:Z

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->data:Ljava/util/ArrayList;

    invoke-virtual {v0}, Ljava/util/ArrayList;->size()I

    move-result v0

    if-ne p1, v0, :cond_0

    const/4 p1, 0x1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method


# virtual methods
.method public addAll(Ljava/util/List;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "TT;>;)V"
        }
    .end annotation

    .line 181
    iget-object v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->data:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->addAll(Ljava/util/Collection;)Z

    .line 182
    invoke-virtual {p0}, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->notifyDataSetChanged()V

    return-void
.end method

.method public addAll(Ljava/util/List;Z)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "TT;>;Z)V"
        }
    .end annotation

    .line 186
    iget-object v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->data:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->addAll(Ljava/util/Collection;)Z

    if-eqz p2, :cond_0

    .line 188
    invoke-virtual {p0}, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->notifyDataSetChanged()V

    :cond_0
    return-void
.end method

.method public areAllItemsEnabled()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method public clear()V
    .locals 1

    .line 193
    iget-object v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->data:Ljava/util/ArrayList;

    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 194
    invoke-virtual {p0}, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->notifyDataSetChanged()V

    return-void
.end method

.method protected abstract doGetView(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
.end method

.method public getCount()I
    .locals 2

    .line 71
    iget-object v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->data:Ljava/util/ArrayList;

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    .line 72
    iget-object v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->data:Ljava/util/ArrayList;

    invoke-virtual {v0}, Ljava/util/ArrayList;->size()I

    move-result v0

    add-int/2addr v1, v0

    .line 74
    :cond_0
    iget-boolean v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->isLoadingData:Z

    if-eqz v0, :cond_1

    add-int/lit8 v1, v1, 0x1

    :cond_1
    return v1
.end method

.method public getData()Ljava/util/ArrayList;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/ArrayList<",
            "TT;>;"
        }
    .end annotation

    .line 177
    iget-object v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->data:Ljava/util/ArrayList;

    return-object v0
.end method

.method public getItem(I)Ljava/lang/Object;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I)TT;"
        }
    .end annotation

    .line 111
    iget-object v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->data:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 114
    :cond_0
    iget-object v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->data:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method public getItemCount()I
    .locals 1

    .line 97
    iget-object v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->data:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 98
    iget-object v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->data:Ljava/util/ArrayList;

    invoke-virtual {v0}, Ljava/util/ArrayList;->size()I

    move-result v0

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public getItemId(I)J
    .locals 2

    int-to-long v0, p1

    return-wide v0
.end method

.method public getItemViewType(I)I
    .locals 0

    .line 165
    invoke-direct {p0, p1}, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->isPositionOfProgressElement(I)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 p1, -0x1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method

.method public getListView()Landroid/widget/AbsListView;
    .locals 1

    .line 55
    iget-object v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->listView:Landroid/widget/AbsListView;

    return-object v0
.end method

.method public getProgressView()Landroid/view/View;
    .locals 1

    .line 59
    iget-object v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->progressView:Landroid/view/View;

    return-object v0
.end method

.method public final getView(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
    .locals 1

    .line 150
    invoke-direct {p0, p1}, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->isPositionOfProgressElement(I)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 151
    iget-object p1, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->progressView:Landroid/view/View;

    return-object p1

    .line 154
    :cond_0
    invoke-virtual {p0, p1, p2, p3}, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->doGetView(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public getViewTypeCount()I
    .locals 1

    const/4 v0, 0x2

    return v0
.end method

.method public hasItems()Z
    .locals 1

    .line 107
    iget-object v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->data:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->data:Ljava/util/ArrayList;

    invoke-virtual {v0}, Ljava/util/ArrayList;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x1

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public isEmpty()Z
    .locals 1

    .line 89
    invoke-virtual {p0}, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->getCount()I

    move-result v0

    if-nez v0, :cond_0

    iget-boolean v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->isLoadingData:Z

    if-nez v0, :cond_0

    const/4 v0, 0x1

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public isEnabled(I)Z
    .locals 0

    .line 123
    invoke-direct {p0, p1}, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->isPositionOfProgressElement(I)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 p1, 0x0

    return p1

    :cond_0
    const/4 p1, 0x1

    return p1
.end method

.method public isLoadingData()Z
    .locals 1

    .line 146
    iget-boolean v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->isLoadingData:Z

    return v0
.end method

.method public remove(I)V
    .locals 1

    .line 198
    iget-object v0, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->data:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->remove(I)Ljava/lang/Object;

    .line 199
    invoke-virtual {p0}, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->notifyDataSetChanged()V

    return-void
.end method

.method public setIsLoadingData(Z)V
    .locals 1

    const/4 v0, 0x1

    .line 135
    invoke-virtual {p0, p1, v0}, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->setIsLoadingData(ZZ)V

    return-void
.end method

.method public setIsLoadingData(ZZ)V
    .locals 0

    .line 139
    iput-boolean p1, p0, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->isLoadingData:Z

    if-eqz p2, :cond_0

    .line 141
    invoke-virtual {p0}, Lcom/github/droidfu/adapters/ListAdapterWithProgress;->notifyDataSetChanged()V

    :cond_0
    return-void
.end method
