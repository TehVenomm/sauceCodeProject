.class final Lcom/helpshift/support/util/PermissionUtil$1;
.super Ljava/lang/Object;
.source "PermissionUtil.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/util/PermissionUtil;->requestPermissions(Landroidx/fragment/app/Fragment;[Ljava/lang/String;ILandroid/view/View;)Lcom/google/android/material/snackbar/Snackbar;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$fragment:Landroidx/fragment/app/Fragment;

.field final synthetic val$permissions:[Ljava/lang/String;

.field final synthetic val$requestCode:I


# direct methods
.method constructor <init>(Landroidx/fragment/app/Fragment;[Ljava/lang/String;I)V
    .locals 0

    .line 50
    iput-object p1, p0, Lcom/helpshift/support/util/PermissionUtil$1;->val$fragment:Landroidx/fragment/app/Fragment;

    iput-object p2, p0, Lcom/helpshift/support/util/PermissionUtil$1;->val$permissions:[Ljava/lang/String;

    iput p3, p0, Lcom/helpshift/support/util/PermissionUtil$1;->val$requestCode:I

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 2

    .line 53
    iget-object p1, p0, Lcom/helpshift/support/util/PermissionUtil$1;->val$fragment:Landroidx/fragment/app/Fragment;

    iget-object v0, p0, Lcom/helpshift/support/util/PermissionUtil$1;->val$permissions:[Ljava/lang/String;

    iget v1, p0, Lcom/helpshift/support/util/PermissionUtil$1;->val$requestCode:I

    invoke-virtual {p1, v0, v1}, Landroidx/fragment/app/Fragment;->requestPermissions([Ljava/lang/String;I)V

    return-void
.end method
