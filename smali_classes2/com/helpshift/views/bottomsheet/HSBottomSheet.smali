.class public Lcom/helpshift/views/bottomsheet/HSBottomSheet;
.super Ljava/lang/Object;
.source "HSBottomSheet.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;
    }
.end annotation


# instance fields
.field final bottomSheet:Landroid/view/ViewGroup;

.field final bottomSheetParentView:Landroid/view/View;

.field final bottomSheetWindow:Landroid/view/Window;

.field callbacks:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/google/android/material/bottomsheet/BottomSheetBehavior$BottomSheetCallback;",
            ">;"
        }
    .end annotation
.end field

.field final contentView:Landroid/view/View;

.field final dimOpacity:F

.field final enableDimAnimation:Z

.field final referenceWindowView:Landroid/view/View;

.field final viewToDim:Landroid/view/View;


# direct methods
.method constructor <init>(Landroid/view/View;Landroid/view/Window;Landroid/view/View;Landroid/view/View;ZFLandroid/view/View;Landroid/view/ViewGroup;)V
    .locals 1
    .param p4    # Landroid/view/View;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    .line 52
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 46
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->callbacks:Ljava/util/List;

    .line 53
    iput-object p1, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->contentView:Landroid/view/View;

    .line 54
    iput-object p2, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->bottomSheetWindow:Landroid/view/Window;

    .line 55
    iput-object p3, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->referenceWindowView:Landroid/view/View;

    .line 56
    iput-object p4, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->viewToDim:Landroid/view/View;

    .line 57
    iput-boolean p5, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->enableDimAnimation:Z

    .line 58
    iput p6, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->dimOpacity:F

    .line 59
    iput-object p7, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->bottomSheetParentView:Landroid/view/View;

    .line 60
    iput-object p8, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->bottomSheet:Landroid/view/ViewGroup;

    return-void
.end method

.method private attachBehaviourCallback()V
    .locals 2

    .line 160
    invoke-virtual {p0}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->getBottomSheetBehaviour()Lcom/google/android/material/bottomsheet/BottomSheetBehavior;

    move-result-object v0

    new-instance v1, Lcom/helpshift/views/bottomsheet/HSBottomSheet$2;

    invoke-direct {v1, p0}, Lcom/helpshift/views/bottomsheet/HSBottomSheet$2;-><init>(Lcom/helpshift/views/bottomsheet/HSBottomSheet;)V

    invoke-virtual {v0, v1}, Lcom/google/android/material/bottomsheet/BottomSheetBehavior;->setBottomSheetCallback(Lcom/google/android/material/bottomsheet/BottomSheetBehavior$BottomSheetCallback;)V

    return-void
.end method

.method private attachBottomSheetToWindow(Landroid/view/ViewGroup$LayoutParams;)V
    .locals 2

    .line 155
    iget-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->bottomSheetWindow:Landroid/view/Window;

    iget-object v1, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->bottomSheetParentView:Landroid/view/View;

    invoke-virtual {v0, v1, p1}, Landroid/view/Window;->addContentView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    return-void
.end method

.method private initiateReferenceViewAttachment()V
    .locals 2

    .line 118
    iget-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->referenceWindowView:Landroid/view/View;

    invoke-static {v0}, Landroidx/core/view/ViewCompat;->isLaidOut(Landroid/view/View;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 119
    invoke-virtual {p0}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->showOnReferenceView()V

    goto :goto_0

    .line 122
    :cond_0
    iget-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->referenceWindowView:Landroid/view/View;

    new-instance v1, Lcom/helpshift/views/bottomsheet/HSBottomSheet$1;

    invoke-direct {v1, p0}, Lcom/helpshift/views/bottomsheet/HSBottomSheet$1;-><init>(Lcom/helpshift/views/bottomsheet/HSBottomSheet;)V

    invoke-virtual {v0, v1}, Landroid/view/View;->post(Ljava/lang/Runnable;)Z

    :goto_0
    return-void
.end method


# virtual methods
.method public addBottomSheetCallback(Lcom/google/android/material/bottomsheet/BottomSheetBehavior$BottomSheetCallback;)V
    .locals 1
    .param p1    # Lcom/google/android/material/bottomsheet/BottomSheetBehavior$BottomSheetCallback;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    .line 107
    iget-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->callbacks:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    return-void
.end method

.method public getBottomSheetBehaviour()Lcom/google/android/material/bottomsheet/BottomSheetBehavior;
    .locals 1

    .line 231
    iget-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->bottomSheet:Landroid/view/ViewGroup;

    invoke-static {v0}, Lcom/google/android/material/bottomsheet/BottomSheetBehavior;->from(Landroid/view/View;)Lcom/google/android/material/bottomsheet/BottomSheetBehavior;

    move-result-object v0

    return-object v0
.end method

.method public getBottomSheetContentView()Landroid/view/View;
    .locals 1

    .line 132
    iget-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->contentView:Landroid/view/View;

    return-object v0
.end method

.method getParamsForReferenceView()Landroid/view/ViewGroup$LayoutParams;
    .locals 2

    .line 148
    iget-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->contentView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v0

    const/4 v1, -0x1

    .line 149
    iput v1, v0, Landroid/view/ViewGroup$LayoutParams;->height:I

    .line 150
    iget-object v1, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->referenceWindowView:Landroid/view/View;

    invoke-virtual {v1}, Landroid/view/View;->getWidth()I

    move-result v1

    iput v1, v0, Landroid/view/ViewGroup$LayoutParams;->width:I

    return-object v0
.end method

.method getParamsForWindow()Landroid/view/ViewGroup$LayoutParams;
    .locals 2

    .line 141
    iget-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->contentView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v0

    const/4 v1, -0x1

    .line 142
    iput v1, v0, Landroid/view/ViewGroup$LayoutParams;->height:I

    .line 143
    iput v1, v0, Landroid/view/ViewGroup$LayoutParams;->width:I

    return-object v0
.end method

.method public remove()V
    .locals 2

    .line 82
    iget-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->bottomSheetParentView:Landroid/view/View;

    invoke-static {v0}, Landroidx/core/view/ViewCompat;->isAttachedToWindow(Landroid/view/View;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 83
    iget-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->bottomSheetParentView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getParent()Landroid/view/ViewParent;

    move-result-object v0

    check-cast v0, Landroid/view/ViewGroup;

    iget-object v1, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->bottomSheetParentView:Landroid/view/View;

    invoke-virtual {v0, v1}, Landroid/view/ViewGroup;->removeView(Landroid/view/View;)V

    .line 86
    :cond_0
    iget-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->viewToDim:Landroid/view/View;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->viewToDim:Landroid/view/View;

    invoke-static {v0}, Landroidx/core/view/ViewCompat;->isAttachedToWindow(Landroid/view/View;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 87
    iget-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->viewToDim:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getParent()Landroid/view/ViewParent;

    move-result-object v0

    check-cast v0, Landroid/view/ViewGroup;

    iget-object v1, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->viewToDim:Landroid/view/View;

    invoke-virtual {v0, v1}, Landroid/view/ViewGroup;->removeView(Landroid/view/View;)V

    :cond_1
    return-void
.end method

.method public removeAllBottomSheetCallbacks()V
    .locals 1

    .line 111
    iget-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->callbacks:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->clear()V

    return-void
.end method

.method public setDraggable(Z)V
    .locals 1

    .line 97
    invoke-virtual {p0}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->getBottomSheetBehaviour()Lcom/google/android/material/bottomsheet/BottomSheetBehavior;

    move-result-object v0

    check-cast v0, Lcom/helpshift/views/bottomsheet/HSBottomSheetBehaviour;

    invoke-virtual {v0, p1}, Lcom/helpshift/views/bottomsheet/HSBottomSheetBehaviour;->setDraggable(Z)V

    return-void
.end method

.method setupBottomSheetView()V
    .locals 5

    const/4 v0, 0x2

    .line 203
    new-array v1, v0, [I

    .line 204
    iget-object v2, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->referenceWindowView:Landroid/view/View;

    invoke-virtual {v2, v1}, Landroid/view/View;->getLocationInWindow([I)V

    .line 207
    iget-object v2, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->bottomSheetWindow:Landroid/view/Window;

    invoke-virtual {v2}, Landroid/view/Window;->getDecorView()Landroid/view/View;

    move-result-object v2

    const/4 v3, 0x0

    if-eqz v2, :cond_0

    const v4, 0x1020002

    .line 209
    invoke-virtual {v2, v4}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v2

    if-eqz v2, :cond_0

    .line 211
    new-array v0, v0, [I

    .line 212
    invoke-virtual {v2, v0}, Landroid/view/View;->getLocationInWindow([I)V

    .line 213
    aget v0, v0, v3

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    .line 222
    :goto_0
    iget-object v2, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->bottomSheetParentView:Landroid/view/View;

    aget v1, v1, v3

    sub-int/2addr v1, v0

    invoke-static {v3, v1}, Ljava/lang/Math;->max(II)I

    move-result v0

    int-to-float v0, v0

    invoke-virtual {v2, v0}, Landroid/view/View;->setX(F)V

    return-void
.end method

.method public show()V
    .locals 3

    .line 67
    iget-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->bottomSheet:Landroid/view/ViewGroup;

    iget-object v1, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->contentView:Landroid/view/View;

    invoke-virtual {v0, v1}, Landroid/view/ViewGroup;->addView(Landroid/view/View;)V

    .line 68
    invoke-direct {p0}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->attachBehaviourCallback()V

    .line 70
    iget-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->referenceWindowView:Landroid/view/View;

    if-eqz v0, :cond_0

    .line 71
    invoke-direct {p0}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->initiateReferenceViewAttachment()V

    goto :goto_0

    .line 74
    :cond_0
    iget-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->bottomSheetWindow:Landroid/view/Window;

    iget-object v1, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->bottomSheetParentView:Landroid/view/View;

    invoke-virtual {p0}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->getParamsForWindow()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/view/Window;->addContentView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    :goto_0
    return-void
.end method

.method showOnReferenceView()V
    .locals 1

    .line 136
    invoke-virtual {p0}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->setupBottomSheetView()V

    .line 137
    invoke-virtual {p0}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->getParamsForReferenceView()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v0

    invoke-direct {p0, v0}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->attachBottomSheetToWindow(Landroid/view/ViewGroup$LayoutParams;)V

    return-void
.end method
