.class final Lnet/gogame/gowrap/ui/fab/FabManager$1;
.super Ljava/lang/Object;
.source "FabManager.java"

# interfaces
.implements Lnet/gogame/gowrap/ui/fab/Fab$ClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/fab/FabManager;->onCreate(Landroid/app/Activity;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$activity:Landroid/app/Activity;


# direct methods
.method constructor <init>(Landroid/app/Activity;)V
    .locals 0

    .line 25
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/FabManager$1;->val$activity:Landroid/app/Activity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Lnet/gogame/gowrap/ui/fab/Fab;Landroid/view/MotionEvent;)V
    .locals 0

    .line 29
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/FabManager$1;->val$activity:Landroid/app/Activity;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/fab/FabManager;->showMenu(Landroid/app/Activity;)V

    return-void
.end method
