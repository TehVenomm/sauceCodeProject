.class Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$2;
.super Ljava/lang/Object;
.source "SupportFormFragment.java"

# interfaces
.implements Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportRequestCollector;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

.field final synthetic val$bodyField:Landroid/widget/EditText;

.field final synthetic val$emailField:Landroid/widget/EditText;

.field final synthetic val$mobileNumberField:Landroid/widget/EditText;

.field final synthetic val$nameField:Landroid/widget/EditText;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;Landroid/widget/EditText;Landroid/widget/EditText;Landroid/widget/EditText;Landroid/widget/EditText;)V
    .locals 0

    .line 129
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$2;->val$nameField:Landroid/widget/EditText;

    iput-object p3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$2;->val$emailField:Landroid/widget/EditText;

    iput-object p4, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$2;->val$mobileNumberField:Landroid/widget/EditText;

    iput-object p5, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$2;->val$bodyField:Landroid/widget/EditText;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public collect()Lnet/gogame/gowrap/support/SupportRequest;
    .locals 8

    .line 133
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$2;->val$nameField:Landroid/widget/EditText;

    invoke-virtual {v0}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gowrap/support/StringUtils;->trimToNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    .line 134
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$2;->val$emailField:Landroid/widget/EditText;

    invoke-virtual {v0}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gowrap/support/StringUtils;->trimToNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 135
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$2;->val$mobileNumberField:Landroid/widget/EditText;

    invoke-virtual {v0}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v0

    .line 136
    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    .line 135
    invoke-static {v0}, Lnet/gogame/gowrap/support/StringUtils;->trimToNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    .line 137
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$2;->val$bodyField:Landroid/widget/EditText;

    invoke-virtual {v0}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gowrap/support/StringUtils;->trimToNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    .line 138
    new-instance v0, Lnet/gogame/gowrap/support/SupportRequest;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-static {v1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->access$100(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Lnet/gogame/gowrap/support/SupportCategory;

    move-result-object v5

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    .line 139
    invoke-static {v1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->access$200(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Landroid/net/Uri;

    move-result-object v7

    move-object v1, v0

    invoke-direct/range {v1 .. v7}, Lnet/gogame/gowrap/support/SupportRequest;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lnet/gogame/gowrap/support/SupportCategory;Ljava/lang/String;Landroid/net/Uri;)V

    return-object v0
.end method
