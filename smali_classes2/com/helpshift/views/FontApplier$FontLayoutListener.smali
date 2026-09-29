.class Lcom/helpshift/views/FontApplier$FontLayoutListener;
.super Ljava/lang/Object;
.source "FontApplier.java"

# interfaces
.implements Landroid/view/ViewTreeObserver$OnGlobalLayoutListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/views/FontApplier;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "FontLayoutListener"
.end annotation


# instance fields
.field private final viewRef:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference<",
            "Landroid/view/View;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Landroid/view/View;)V
    .locals 1

    .line 92
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 93
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object v0, p0, Lcom/helpshift/views/FontApplier$FontLayoutListener;->viewRef:Ljava/lang/ref/WeakReference;

    return-void
.end method


# virtual methods
.method public onGlobalLayout()V
    .locals 1

    .line 98
    iget-object v0, p0, Lcom/helpshift/views/FontApplier$FontLayoutListener;->viewRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/view/View;

    if-nez v0, :cond_0

    return-void

    .line 102
    :cond_0
    invoke-static {v0}, Lcom/helpshift/views/FontApplier;->applyInternal(Landroid/view/View;)V

    return-void
.end method
