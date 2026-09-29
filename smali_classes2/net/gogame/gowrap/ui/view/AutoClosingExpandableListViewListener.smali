.class public Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;
.super Ljava/lang/Object;
.source "AutoClosingExpandableListViewListener.java"

# interfaces
.implements Landroid/widget/ExpandableListView$OnGroupExpandListener;


# instance fields
.field private lastExpandedPosition:I

.field private final parent:Landroid/widget/ExpandableListView;


# direct methods
.method public constructor <init>(Landroid/widget/ExpandableListView;Landroid/os/Bundle;)V
    .locals 1

    .line 14
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, -0x1

    .line 10
    iput v0, p0, Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;->lastExpandedPosition:I

    .line 16
    iput-object p1, p0, Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;->parent:Landroid/widget/ExpandableListView;

    if-eqz p2, :cond_0

    const-string p1, "lastExpandedPosition"

    .line 18
    invoke-virtual {p2, p1, v0}, Landroid/os/Bundle;->getInt(Ljava/lang/String;I)I

    move-result p1

    iput p1, p0, Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;->lastExpandedPosition:I

    :cond_0
    return-void
.end method


# virtual methods
.method public onGroupExpand(I)V
    .locals 2

    .line 24
    iget v0, p0, Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;->lastExpandedPosition:I

    const/4 v1, -0x1

    if-eq v0, v1, :cond_0

    iget v0, p0, Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;->lastExpandedPosition:I

    if-eq p1, v0, :cond_0

    .line 25
    iget-object v0, p0, Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;->parent:Landroid/widget/ExpandableListView;

    iget v1, p0, Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;->lastExpandedPosition:I

    invoke-virtual {v0, v1}, Landroid/widget/ExpandableListView;->collapseGroup(I)Z

    .line 27
    :cond_0
    iput p1, p0, Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;->lastExpandedPosition:I

    return-void
.end method

.method public saveState(Landroid/os/Bundle;)V
    .locals 2

    const-string v0, "lastExpandedPosition"

    .line 31
    iget v1, p0, Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;->lastExpandedPosition:I

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    return-void
.end method
