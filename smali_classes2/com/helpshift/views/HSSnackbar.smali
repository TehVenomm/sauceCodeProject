.class public Lcom/helpshift/views/HSSnackbar;
.super Ljava/lang/Object;
.source "HSSnackbar.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 7
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static make(Landroid/view/View;II)Lcom/google/android/material/snackbar/Snackbar;
    .locals 1
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    .line 18
    invoke-virtual {p0}, Landroid/view/View;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    invoke-virtual {v0, p1}, Landroid/content/res/Resources;->getText(I)Ljava/lang/CharSequence;

    move-result-object p1

    invoke-static {p0, p1, p2}, Lcom/helpshift/views/HSSnackbar;->make(Landroid/view/View;Ljava/lang/CharSequence;I)Lcom/google/android/material/snackbar/Snackbar;

    move-result-object p0

    return-object p0
.end method

.method public static make(Landroid/view/View;Ljava/lang/CharSequence;I)Lcom/google/android/material/snackbar/Snackbar;
    .locals 0
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    .line 11
    invoke-static {p0, p1, p2}, Lcom/google/android/material/snackbar/Snackbar;->make(Landroid/view/View;Ljava/lang/CharSequence;I)Lcom/google/android/material/snackbar/Snackbar;

    move-result-object p0

    .line 12
    invoke-virtual {p0}, Lcom/google/android/material/snackbar/Snackbar;->getView()Landroid/view/View;

    move-result-object p1

    invoke-static {p1}, Lcom/helpshift/views/FontApplier;->apply(Landroid/view/View;)V

    return-object p0
.end method
