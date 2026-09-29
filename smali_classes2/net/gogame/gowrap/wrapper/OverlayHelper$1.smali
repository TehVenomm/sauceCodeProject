.class Lnet/gogame/gowrap/wrapper/OverlayHelper$1;
.super Ljava/lang/Object;
.source "OverlayHelper.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/wrapper/OverlayHelper;->show(III)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/wrapper/OverlayHelper;

.field final synthetic val$gravity:I

.field final synthetic val$x:I

.field final synthetic val$y:I


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/wrapper/OverlayHelper;III)V
    .locals 0

    .line 49
    iput-object p1, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper$1;->this$0:Lnet/gogame/gowrap/wrapper/OverlayHelper;

    iput p2, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper$1;->val$gravity:I

    iput p3, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper$1;->val$x:I

    iput p4, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper$1;->val$y:I

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    .line 53
    iget-object v0, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper$1;->this$0:Lnet/gogame/gowrap/wrapper/OverlayHelper;

    iget v1, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper$1;->val$gravity:I

    iget v2, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper$1;->val$x:I

    iget v3, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper$1;->val$y:I

    invoke-virtual {v0, v1, v2, v3}, Lnet/gogame/gowrap/wrapper/OverlayHelper;->show(III)V

    return-void
.end method
