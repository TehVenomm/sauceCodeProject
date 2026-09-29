.class Lnet/gogame/gowrap/ui/MainActivity$8;
.super Ljava/lang/Object;
.source "MainActivity.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/MainActivity;->onCreate(Landroid/os/Bundle;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/MainActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/MainActivity;)V
    .locals 0

    .line 123
    iput-object p1, p0, Lnet/gogame/gowrap/ui/MainActivity$8;->this$0:Lnet/gogame/gowrap/ui/MainActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 0

    .line 127
    sget-object p1, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {p1}, Lnet/gogame/gowrap/GoWrapImpl;->hasOffers()Z

    move-result p1

    if-eqz p1, :cond_0

    .line 128
    sget-object p1, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {p1}, Lnet/gogame/gowrap/GoWrapImpl;->showOffers()V

    :cond_0
    return-void
.end method
