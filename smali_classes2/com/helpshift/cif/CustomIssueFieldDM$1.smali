.class Lcom/helpshift/cif/CustomIssueFieldDM$1;
.super Lcom/helpshift/common/domain/F;
.source "CustomIssueFieldDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/cif/CustomIssueFieldDM;->setCustomIssueFieldData(Ljava/util/Map;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/cif/CustomIssueFieldDM;

.field final synthetic val$data:Ljava/util/Map;


# direct methods
.method constructor <init>(Lcom/helpshift/cif/CustomIssueFieldDM;Ljava/util/Map;)V
    .locals 0

    .line 53
    iput-object p1, p0, Lcom/helpshift/cif/CustomIssueFieldDM$1;->this$0:Lcom/helpshift/cif/CustomIssueFieldDM;

    iput-object p2, p0, Lcom/helpshift/cif/CustomIssueFieldDM$1;->val$data:Ljava/util/Map;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 56
    iget-object v0, p0, Lcom/helpshift/cif/CustomIssueFieldDM$1;->this$0:Lcom/helpshift/cif/CustomIssueFieldDM;

    iget-object v1, p0, Lcom/helpshift/cif/CustomIssueFieldDM$1;->val$data:Ljava/util/Map;

    invoke-static {v0, v1}, Lcom/helpshift/cif/CustomIssueFieldDM;->access$000(Lcom/helpshift/cif/CustomIssueFieldDM;Ljava/util/Map;)Ljava/util/ArrayList;

    move-result-object v0

    .line 57
    iget-object v1, p0, Lcom/helpshift/cif/CustomIssueFieldDM$1;->this$0:Lcom/helpshift/cif/CustomIssueFieldDM;

    invoke-static {v1}, Lcom/helpshift/cif/CustomIssueFieldDM;->access$100(Lcom/helpshift/cif/CustomIssueFieldDM;)Lcom/helpshift/cif/dao/CustomIssueFieldDAO;

    move-result-object v1

    invoke-interface {v1, v0}, Lcom/helpshift/cif/dao/CustomIssueFieldDAO;->setCustomIssueFields(Ljava/util/ArrayList;)V

    return-void
.end method
